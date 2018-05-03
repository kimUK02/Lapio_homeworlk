# Hackserschool FTZ level5
---
- 힌트를 보면 
>/usr/bin/level5 프로그램은 /tmp 디렉토리에
 level5.tmp 라는 이름의 임시파일을 생성한다.
>
>이를 이용하여 level6의 권한을 얻어라.

- 그렇다면 
>$ cd /usr/bin/
>
>$ ls level5

하면  나온다.

힌트에서 level5를 실행하면 
/tmp에 level5.tmp가 생성된다고 하니까 

/usr/bin/에서
>$ ./level5

를 하고 
>$ cd /tmp
>ls 

를 하면 
level5.tmp가 보인....??

안보인다 
- 그러면 만들어야지
>$ vim level5.tmp
해서 
~~~
아무거나 
~~~
이렇게 만들면 
다시 실행할때 덮어쓰일 것이다.

다시 
>$ cd /usr/bin 
>
>$ ./level5

하면  
>$ cd /tmp

에 level5.tmp 가 생기고 
>$ cat level5.tmp 

답이나온다.