# Hackserschool FTZ level2
 ---
 - 이전과같이 
 >$ ls -al 

이렇게 입력하면 
hint가 보인다.
>텍스트 파일 편집 중 쉘의 명령을 실행시킬 수 있다는데...

라는 문구가 보인다 아무튼

- level3 권한을 찾으면 
>$ find / -user level3 -perm -4000 2> /dev/null

**/usr/bin/editor**
가 나온다. 

- 그래서 level1과 마찬가지로 cd를 해봤는데 역시 editor는 디렉터리가 아니였다    
  - 그래서 editor을 실행해봤더니  
    >$ ./editor
    
     vi가 나왔다.


- 그래서 :을 입력하고 !를 쓰면 외부의 쉘을 실행시켜준다 

    *즉 level3의 /bin/bash 로 접속하게 해준다*

    여기서 my-pass 를 입력하면된다

 

