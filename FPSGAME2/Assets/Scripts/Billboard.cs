using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform target;//카메라
    // Update is called once per frame
    void Update()
    {
        //자기자신과 카메라의 방향을 이치시킨다
        transform.forward = target.forward;
    }
}
