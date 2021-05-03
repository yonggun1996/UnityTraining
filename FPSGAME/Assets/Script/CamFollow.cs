using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        //카메라의 위치를 플레이어와 같이 위치시킨다
        transform.position = target.position;
    }
}
