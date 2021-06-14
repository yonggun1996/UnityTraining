using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        print("충돌된 객체 : " + col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            print("아이템 획득");
            GameObject.Find("Player").GetComponent<PlayerMove>().addHP();
            Destroy(this.gameObject);
        }
    }
}
