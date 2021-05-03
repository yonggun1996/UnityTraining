using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition; //발사위치
    public GameObject bombFactory; //투척 무기 오브젝트 
    public float throwPower = 15f; // 투척 파워
    public GameObject bulletEffect;
    ParticleSystem ps;

    void start()
    {
        print("ps 생성");
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))//0:왼쪽 1:오른쪽 2: 마우스 휠
        {
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }

        if (Input.GetMouseButtonDown(0))
        {
            //레이를 생성한 후 발사될 위치와 진행 방향을 설정함
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo = new RaycastHit();
            //레이를 발사한 후 부딥힌 물체의 정보를 hitinfo에 저장
            if(Physics.Raycast(ray, out hitInfo))
            {
                bulletEffect.transform.position = hitInfo.point;
                ps.Play();
            }
        }
    }
}
