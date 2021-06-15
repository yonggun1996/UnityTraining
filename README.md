# UnityTraining
## 명지전문대학교 게임프로그래밍 수업 소스코드

### 1학기 후반(4월 26 ~ 6월 14 예정) FPS 게임 만들기 실습

-4월 26일 수업내용

  -마우스를 이용한 카메라 회전
  Transform.eulerAngles를 사용 (소스코드 Camera 참고)
  
  -캐릭터 이동
  TransformDirection 사용 (소스코드 PlayerMove 참고)
  
-5월 10일 수업내용

  -코루틴 : 원래 프로그램 순서는 일반적으로 불려지는 쪽이 부르는 쪽에 속하는 것이 대부분이지만 서로 대등한 관계로 호출되는것을 말합니다.(출처 : https://terms.naver.com/entry.naver?docId=819147&cid=50376&categoryId=50376)
  
  -코루틴 함수구문 : IEnumerator 함수이름 (인자값)
  -코루틴 리턴 : yield return 으로 시작하는 구문들이다.
  
  ```C#
  IEnumerator PlayerHitEffect()
    {
        hitEffect.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        hitEffect.SetActive(false);
    }
    ```
    
이 코드는 hiteffect를 0.3초동안 활성화 한 후 비활성화로 바꾸는 코드입니다.

```C#
if (hp > 0)
{
    StartCoroutine(PlayerHitEffect());
}
```

이렇게 PlayerHitEffent()를 호출해 코루틴 함수를 실행합니다.
  
- 기말고사 과제물
자세한 내용은 해당 ppt를 참고하시면 됩니다. 
[2021년 기말고사 게임계획서_2021531010 박용건.pptx](https://github.com/yonggun1996/UnityTraining/files/6652969/2021._2021531010.pptx)

또한 Report 폴더를 열어 내용을 확인할 수 있습니다.

요구사항 1 : 배경 꾸미기
요규사항 2 : 게임 난이도 조절
요구사항 3 : 총앨 개수 제한, UI 구현
요구사항 4 : 아이템 추가
요구사항 5 : 씬 추가
요구사항 6 : 그 외 내가 추가하고 싶은 기능 추가하기
