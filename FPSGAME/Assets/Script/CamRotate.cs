﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotSpeed = 2001;
    float mx = 0;
    float my = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //X와 Y를 입력
        float mouse_X = Input.GetAxis("Mouse X");//상하를 나타낸다
        float mouse_Y = Input.GetAxis("Mouse Y");//좌우를 나타낸다

        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;
        my = Mathf.Clamp(my, -90f, 90f);
        //입력값을 이용해 회전 방향을 결정한다
        //Vector3 dir = new Vector3(-mouse_Y, mouse_X, 0);//마우스를 내리면 고개를 내리고, 반대면 고개를 올려야해서 -를 붙였다

        //회전방향으로 물체를 회전시킨다
        //transform.eulerAngles += dir * rotSpeed * Time.deltaTime;

        /*
        Vector3 rot = transform.eulerAngles;
        rot.x = Mathf.Clamp(rot.x, -90f, 90f);//-90도 ~ +90도 까지 제한을둔다
        transform.eulerAngles = rot;
        */

        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}