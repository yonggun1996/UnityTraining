using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotSpeed = 200f;
    float mx = 0;
   
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
       
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
