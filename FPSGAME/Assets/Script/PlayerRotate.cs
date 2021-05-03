using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotSpeed = 2001;
    float mx = 0;
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
        
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
