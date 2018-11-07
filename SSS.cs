using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Threading;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.Drawing;
using System.Drawing.Imaging;

namespace ProsessList {

    public partial class Form1 : Form {
        bool isKill = true;
        int DClickNum = 0;
        public string TargetPath;
        StringBuilder sb = new StringBuilder();

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Thread worker = new Thread(new ThreadStart(StartUpMaker));
            worker.Start();
            TimerInit();
            SetTargetPath();
            CheckProcess();
            //label3.Text = SystemInformation.ComputerName;
            //SendLog();
            SendPhoto();
        }

        private void Timer1_Tick(object sender, EventArgs e) {
            sb.Remove(0, sb.Length);
            textBox1.Text = string.Empty;
            CheckProcess();
        }
        private void TimerInit() {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() {
                Interval = 10000,
            };
            timer.Tick += new EventHandler(Timer1_Tick);
            timer.Start();
        }

        public void CheckProcess() {
            try {
                foreach (Process p in Process.GetProcesses()) {
                    WriteProcessinfo(p);
                    ProcKill(p);
                    textBox1.Text = sb.ToString();
                }
            } catch {

            }
        }
        private void WriteProcessinfo(Process processinfo) {
            try {
                sb.AppendLine(processinfo.MainModule.FileName + "\n");
            } catch (System.ComponentModel.Win32Exception) {
                sb.AppendLine(processinfo.ProcessName.Normalize());
            }
        }

        private void StartUpMaker() {
            try {
                // 시작프로그램 등록하는 레지스트리
                string runKey = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run";
                RegistryKey strUpKey = Registry.LocalMachine.OpenSubKey(runKey, RegistryKeyPermissionCheck.ReadWriteSubTree);

                if (strUpKey.GetValue("StartupTest") == null) {
                    strUpKey.Close();
                    strUpKey = Registry.LocalMachine.OpenSubKey(runKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
                    // 시작프로그램 등록명과 exe경로를 레지스트리에 등록
                    strUpKey.SetValue("StartupTest", Application.ExecutablePath);
                }
                MessageBox.Show("Add Startup Success");
            } catch {
                MessageBox.Show("Add Startup Fail");
            }
        }

        private void SetTargetPath() {
            Dictionary<string, string> clientType = new Dictionary<string, string> {
                { @"SOFTWARE\WOW6432Node\Riot Games, inc\League of Legends", "Location" },
                { @"SOFTWARE\WOW6432Node\Riot Games\RADS", @"LocalRootFolder" }
            };

            string path = "";
            RegistryKey clientKey;

            foreach (var key in clientType.Keys) {
                clientKey = Registry.LocalMachine.OpenSubKey(key);
                label1.Text += clientType[key] + "\n" + key + "\n";

                if (clientKey != null) {
                    path = clientKey.GetValue(clientType[key]).ToString();
                }
                if (path != string.Empty) {
                    TargetPath = path.Replace("/", "\\");
                    label1.Text = TargetPath;
                }
            }
        }
        private void ProcKill(Process p) {
            try {
                string Procpath = p.MainModule.FileName;
                if (Procpath.Contains(TargetPath)) {
                    p.Kill();
                    //SendLog();
                }
            } catch (System.ComponentModel.Win32Exception) {
                label2.Text = "NOT FOUND";
            } catch (ArgumentNullException) {
                label2.Text = "NOT FOUND";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            //Application.Restart();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            if (isKill) Application.Restart();
        }

        private void Label2_DoubleClick(object sender, EventArgs e) {
            DClickNum++;
            if (DClickNum > 1) isKill = false;
        }
        private void SendPhoto() {
            string url = @"http://192.168.137.236:6974/img";
            WebClient webClient = new WebClient();
            webClient.UploadData(url, Copy());            
        }
        private void SendLog() {
            string url = @"http://192.168.137.236:6974/log";
            string data = string.Format("{{ \"name\": \"{0}\", \"mac\" : \"{1}\" }}", SystemInformation.ComputerName, GetMacAddress());
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Timeout = 30 * 1000;


            // POST할 데이타를 Request Stream에 쓴다
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            request.ContentLength = bytes.Length; // 바이트수 지정

            using (Stream reqStream = request.GetRequestStream()) {
                reqStream.Write(bytes, 0, bytes.Length);
            }

            // Response 처리
            string responseText = string.Empty;
            using (WebResponse resp = request.GetResponse()) {
                Stream respStream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream)) {
                    responseText = sr.ReadToEnd();
                }
            }
            label3.Text = responseText;
        }

        private static string GetMacAddress() {
            return NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString();
        }
        private static string Macformat(string str) {
            string mac = "";

            char[] chrArr = str.ToCharArray();
            for (int i = 0; i < chrArr.Length; i++) {
                if (i % 2 == 0) {
                    mac += chrArr[i].ToString();
                } else {
                    mac += chrArr[i].ToString();
                    if (i != chrArr.Length - 1)
                        mac += "-";
                }
            }
            return mac;
        }

        public static byte[] Copy() {
            byte[] result = null;
            // 주화면의 크기 정보 읽기
            Rectangle rect = Screen.PrimaryScreen.Bounds;
            // 2nd screen = Screen.AllScreens[1]

            // 픽셀 포맷 정보 얻기 (Optional)
            int bitsPerPixel = Screen.PrimaryScreen.BitsPerPixel;
            PixelFormat pixelFormat = PixelFormat.Format32bppArgb;
            if (bitsPerPixel <= 16) {
                pixelFormat = PixelFormat.Format16bppRgb565;
            }
            if (bitsPerPixel == 24) {
                pixelFormat = PixelFormat.Format24bppRgb;
            }

            // 화면 크기만큼의 Bitmap 생성
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, pixelFormat);

            // Bitmap 이미지 변경을 위해 Graphics 객체 생성
            using (Graphics gr = Graphics.FromImage(bmp)) {
                // 화면을 그대로 카피해서 Bitmap 메모리에 저장
                gr.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size);
            }

            // Bitmap 데이타를 파일로 저장
            //bmp.Save(outputFilename);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            result = ms.ToArray();
            bmp.Dispose();
            return result;

        }
    }
}
