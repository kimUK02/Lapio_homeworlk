# Hackserschool FTZ level1
 ---

 - log in 해서 현재 디렉토리의  파일들을 보면 
>[level1@ftz level1]$ ls -al
>>-rw-r--r-- 1 root 47 Apr  4  2000 hint 

가보이는 데 이것을 vi 로 보면 

>**힌트는 level2 권한에 setuid가 걸린 파일을 찾는다.**

라는 문구가 보인다. 
*SetUID란: 임시적인 root권한을 가진 ID이다.*

- 그래서 find 명령어를쓰면 
>$ find / -user level -perm -4000 2> /bin/null

여기서
>>2> /bin/null
은 표준에러(2)를 
/bin/null 즉 표준에러는 쓰래기 통에 버리라는 뜻이다.

아무튼 이렇게 명령어를 쓰면 */bin/ExecuteMe*
라고 뜨는데 
>$ cd /bin/ExecuteMe 

라고 하면 에러가 나오는데

**이 뜻은 ExecuteMe가 디렉터리가 아니라는 뜻이다.**

- 그래서 ExecuteMe를 실행해보았다.
>$ cd /bin 
>$./ExecuteMe


                레벨2의 권한으로 당신이 원하는 명령어를
                한가지 실행시켜 드리겠습니다.
                (단, my-pass 와 chmod는 제외)

                어떤 명령을 실행시키겠습니까?


                [level2@ftz level2]$
                레벨2의 권한으로 당신이 원하는 명령어를
                한가지 실행시켜 드리겠습니다.
                (단, my-pass 와 chmod는 제외)

                어떤 명령을 실행시키겠습니까?


                [level2@ftz level2]$

라고 하고있는데 

- 마지막에  **[level2@ftz level2]$** 라고 하는것이 있다 그래서 level2의 /bin/bash *즉 명령어를 실행할수 있는 곳??* ~~맞나?~~
으로가면 
>$ /bin/bash

level2의 셀이 보인다. 여기서 my-pass 를 쓰면 
통과된다. 
>$ my-pass

