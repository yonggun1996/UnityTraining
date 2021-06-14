using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine1 : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            print("탄창을 주웠습니다.");
            GameObject.Find("Player").GetComponent<PlayerFire>().now_bulletcount += 20;
            //Destroy(transform.parent.gameObject);
            transform.parent.gameObject.SetActive(false);
        }
        
    }
}
