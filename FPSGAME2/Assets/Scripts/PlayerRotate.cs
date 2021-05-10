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
        // 1.마우스 입렵값을 받는다.
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");
        mx += mouse_X * rotSpeed * Time.deltaTime;
       
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
