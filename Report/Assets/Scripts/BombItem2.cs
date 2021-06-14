using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem2 : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            print("수류탄을 주웠습니다.");
            GameObject.Find("Player").GetComponent<PlayerFire2>().now_bombcount += 10;
            //Destroy(transform.parent.gameObject);
            //transform.parent.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
