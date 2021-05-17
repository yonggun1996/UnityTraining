using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition; //발사위치
    public GameObject bombFactory; //투척 무기 오브젝트 
    public float throwPower = 15f; // 투척 파워
    public GameObject bulletEffect; // 피격 이펙트 오브젝트
    ParticleSystem ps; // 피격 이펙트 파티클 시스템
    public int weaponPower = 5;//총알 공격력
    private void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            //게임 실행하는 상태가 아니라면 아래 내용들을 실행하지 않는다(움직이지 않는다)
            return;
        }

        if (Input.GetMouseButtonDown(1))//0:왼쪽 1:오른쪽 2: 마우스 휠
        {
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }
        if (Input.GetMouseButtonDown(0))
        { 
            //레이를 생성한 후 발사될 위ㅣ와 진행 방향을 설정함
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo = new RaycastHit();
            //레이를 발사한 후 부딪힌 물체의 정보를 hitInfo에 저장
            if (Physics.Raycast(ray, out hitInfo))
            {
                //적 물체와 부딪혔다면
                if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM eFsm = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFsm.HitEnemy(weaponPower);
                }else
                {
                    bulletEffect.transform.position = hitInfo.point;
                    bulletEffect.transform.forward = hitInfo.normal;
                    ps.Play();
                }
          //      print(hitInfo.point);
            }
        }
    }
}
