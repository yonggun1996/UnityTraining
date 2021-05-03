using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f;
    CharacterController cc;
    float gravity = -20f;//중력과 관련된 변수
    float yVelocity = 0f;//수직속력변수
    public float jumpPower = 10f;
    public bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //사용자 입력 받기
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //이동방향 설정
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;//정규화

        //이동속도에 맞춰 이동한다. p = p0 + vt
        dir = Camera.main.transform.TransformDirection(dir);
        //transform.position += dir * moveSpeed * Time.deltaTime;

        if(cc.collisionFlags == CollisionFlags.Below)
        {
            if (isJumping)
            {
                isJumping = false;
            }
            yVelocity = 0;
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }

        //캐릭터 수직 속도에 중력값을 적용한다
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;
        cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}
