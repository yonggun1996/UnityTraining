using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItemManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<PlayerFire>().now_bombcount == 0)
        {
            print("수류탄을 다 사용해 새로 생성합니다.");
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform mz = transform.GetChild(i);
                mz.gameObject.SetActive(true);
            }
        }
    }
}
