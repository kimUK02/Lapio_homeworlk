using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace SpyDer {

	public partial class Form1 : Form {
        public string currentKey = (string)Registry.GetValue(
            @"HKEY_CURRENT_USER\System\GameConfigStore\Children\e9b84f65-ea7d-4740-84f7-e8204de239fd",
            "MatchedExeFullPath",
            null);
        
        Process[] allProc = Process.GetProcesses();
		StringBuilder sb = new StringBuilder();
		StringBuilder allFile = new StringBuilder();

		public Form1() {
			InitializeComponent();
			this.Opacity = 0.5;
			this.ShowInTaskbar = false;
		}

		private void Form1_Load(object sender, EventArgs e) {

			try {
				// 시작프로그램 등록하는 레지스트리
				string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                string runkeyTest = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run";
                RegistryKey strUpKey2 = Registry.LocalMachine.OpenSubKey(runkeyTest);
                RegistryKey strUpKey = Registry.LocalMachine.OpenSubKey(runKey);
				if (strUpKey.GetValue("StartupTest") == null) {
                    strUpKey2.Close();
                    strUpKey2 = Registry.LocalMachine.OpenSubKey(runkeyTest, true);
                    strUpKey2.SetValue("Test2", (Application.ExecutablePath).ToString());
                    strUpKey.Close();
					strUpKey = Registry.LocalMachine.OpenSubKey(runKey, true);
					// 시작프로그램 등록명과 exe경로를 레지스트리에 등록
					strUpKey.SetValue("StartupTest", (Application.ExecutablePath).ToString());
                    strUpKey2.SetValue("Test2", (Application.ExecutablePath).ToString());
                }
				MessageBox.Show("Add Startup Success");
			} catch {
				MessageBox.Show("Add Startup Fail");
			}

			textBox1.ScrollBars = ScrollBars.Vertical;
			textBox2.ScrollBars = ScrollBars.Vertical;
			Timer timer = new Timer() {
				Interval = 15000
			};
			timer.Tick += new EventHandler(Timer1_Tick);
			timer.Start();
		}

        //C:\Riot Games\League of Legends\RADS\projects\league_client\releases\0.0.0.153\deploy\LeagueClientUx.exe
        public void ProcessList() {
            
            if (currentKey == null) {
                foreach (Process p in allProc) {
                    sb.AppendLine("Process name: " + p.ProcessName);
                    try {
                        sb.AppendLine("Path >> " + p.MainModule.FileName);
                        sb.AppendLine(" ");
                    } catch {
                        sb.AppendLine("Path >> Process is (86x)");
                        sb.AppendLine(" ");
                        label1.Text = "NOT FOUND";
                    }
                }
            } else {
                currentKey = currentKey.Replace(@"\LeagueClientUx.exe", null);
                string[] entries = Directory.GetFiles(currentKey, "*.exe", SearchOption.TopDirectoryOnly);
                foreach (var f in entries) {
                    allFile.AppendLine(f);
                }
                foreach (Process p in allProc) {
                    sb.AppendLine("Process name: " + p.ProcessName);
                    try {
                        sb.AppendLine("Path >> " + p.MainModule.FileName);
                        sb.AppendLine(" ");
                        if (entries.Contains(p.MainModule.FileName)) p.Kill();
                    } catch {
                        sb.AppendLine("Path >> Process is (86x)");
                        sb.AppendLine(" ");
                    } finally {
                        label1.Text = currentKey;
                    }
                }
            }
		}

		private void Timer1_Tick(object sender, EventArgs e) {
			sb.Remove(0, sb.Length);
			allFile.Remove(0, allFile.Length);
			ProcessList();
			textBox1.Text = string.Empty;
			textBox2.Text = string.Empty;
			textBox1.Text = sb.ToString();
			textBox2.Text = allFile.ToString();
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
			Application.Restart();
		}
	}
}
