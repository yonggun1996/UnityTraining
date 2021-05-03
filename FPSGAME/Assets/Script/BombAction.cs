using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombEffect;//폭발 이펙트 프리팹 변수
    void onCollisionEnter(Collision collision)
    {
        GameObject eff = Instantiate(bombEffect); //이펙트 프리팹 생성
        eff.transform.position = transform.position; // 수류탄 오브젝트 위치와 동일
        Destroy(gameObject); //자기자신을 제거한다,
    }
}
