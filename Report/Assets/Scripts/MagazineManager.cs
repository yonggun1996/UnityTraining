using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Player").GetComponent<PlayerFire>().now_bulletcount == 0)
        {
            print("탄창을 다 획득해 새로 생성합니다.");
            for(int i = 0; i < transform.childCount; i++)
            {
                Transform mz = transform.GetChild(i);
                mz.gameObject.SetActive(true);
            }
        }
    }
}
