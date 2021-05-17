using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f;
    CharacterController cc;
    float gravity = -20f; //중력 변수
    float yVelocity = 0f; //수직 속력 변수
    public float jumpPower = 10f;
    public bool isJumping = false;
    public int hp = 20;//플레이어의 체력
    int maxHp = 20;
    public Slider hpSlider;
    public GameObject hitEffect;
    
    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gm.gState != GameManager.GameState.Run)
        {
            //게임 실행하는 상태가 아니라면 아래 내용들을 실행하지 않는다(움직이지 않는다)
            return;
        }

        // 1. 사용자 입력
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //2. 이동 방향 설정
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized; // 정규화
       // 3. 이동속도에 맞춰 이동한다. p =p0 + vt
        dir = Camera.main.transform.TransformDirection(dir);
        //transform.position += dir * moveSpeed * Time.deltaTime;

        if (cc.collisionFlags == CollisionFlags.Below)
        {
            if (isJumping)
            {
                isJumping = false;
                //yVelocity = 0;
            }
            yVelocity = 0;
        }
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }

        // 캐릭터 수직 속도에 중력 값을 적용한다.
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;
      //  print("yVelocity: " + yVelocity);
        cc.Move(dir * moveSpeed * Time.deltaTime);
        hpSlider.value = (float)hp / (float)maxHp;
    }

    public void DamageAction(int damage)
    {
        hp -= damage;
        if(hp > 0)
        {
            StartCoroutine(PlayHitEffect());
        }
    }

    IEnumerator PlayHitEffect()
    {
        hitEffect.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        hitEffect.SetActive(false);
    }
}
