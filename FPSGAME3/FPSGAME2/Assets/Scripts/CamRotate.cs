using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotSpeed = 200f;
    float mx = 0;
    float my = 0;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            //게임 실행하는 상태가 아니라면 아래 내용들을 실행하지 않는다(움직이지 않는다)
            return;
        }

        // 1.마우스 입렵값을 받는다.
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");
        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;
        my = Mathf.Clamp(my, -90f, 90f);
        //2 마우스 입력값을 이용해 회전 방향을 결정한다.
       // Vector3 dir = new Vector3(-mouse_Y, mouse_X, 0);
        //회전방향으로 물체를 회전시킨다.
      //  transform.eulerAngles += dir * rotSpeed * Time.deltaTime;
        /*
        Vector3 rot = transform.eulerAngles;
        rot.x = Mathf.Clamp(rot.x, -90f, 90f);
        transform.eulerAngles = rot;
        */
        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
