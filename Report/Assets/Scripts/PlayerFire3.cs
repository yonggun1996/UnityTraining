using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerFire3 : MonoBehaviour
{
    public GameObject firePosition; // 발사 위치
    public GameObject bombFactory; // 수류탄 프리팹
    public float throwPower = 15f;
    public int weaponPower = 5;
    public GameObject bulletEffect; // 피격 이펙트 오브젝트
    ParticleSystem ps;//피격 이펙트 파티클 시스템
    Animator anim;
    public Text wModeText;
    public Text nowBulletText;
    public Text nowBombText;
    public GameObject[] eff_Flash;
    public int now_bulletcount = 20;
    public int now_bombcount = 10;

    enum WeponMode //무기 모드 변수
    {
        Normal,
        Sniper
    }
    WeponMode wMode;
    bool ZoomMode = false;
    private void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();
        wMode = WeponMode.Normal;
        nowBulletText.text = "총알 개수 : " + now_bulletcount;
        nowBombText.text = "수류탄 개수 : " + now_bombcount;
    }
    // Update is called once per frame
    void Update()
    {
        //게임 상태가 '게임 중' 상태일 때만 조작할 수 있게 한다.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }
        //노멀모드: 마우스 오른쪽 버튼을 누르면 시선 방향으로 수류탄을 던진다.
        //스나이퍼 모드: 마우스 오른쪽 버튼을 누르면 화면을 확대하고 싶다.
        if (Input.GetMouseButtonDown(1))//오른쪽 마우스버튼 눌렀다면
        {
            switch (wMode)
            {
                case WeponMode.Normal:
                    if(now_bombcount > 0)
                    {
                        GameObject bomb = Instantiate(bombFactory); // 수류탄 생성
                        bomb.transform.position = firePosition.transform.position;
                        //수류탄 오브젝트의 Rigidbody 컴포넌트를 가져온다.
                        Rigidbody rb = bomb.GetComponent<Rigidbody>();
                        //카메라의 정면 방향으로 수류탄에 물리적인 힘을 가한다.
                        --now_bombcount;
                        rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
                    }
                    break;
                case WeponMode.Sniper:
                    //만일, 줌 모드 상태가 아니라면 카메라를 확대하고 줌 모드 상태로 변경한다.
                    if (!ZoomMode)
                    {
                        Camera.main.fieldOfView = 15f;
                        ZoomMode = true;
                    }
                    else
                    {
                        Camera.main.fieldOfView = 60f;
                        ZoomMode = false;
                    }
                    break;
            }

            
        }
        if (Input.GetMouseButtonDown(0)) //왼쪽 마우스버튼을 눌렀다면..
        {
            if(now_bulletcount > 0)
            {
                if (anim.GetFloat("MoveMotion") == 0)
                {
                    anim.SetTrigger("Attack");
                }
                //레이를 생성한 후 발사될 위치와 진행 방향을 설정한다.
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                //레이가 부딪힌 대상의 정보를 저장할 변수를 생성한다.
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        EnemyFSM3 eFSM = hitInfo.transform.GetComponent<EnemyFSM3>();
                        eFSM.HitEnemy(weaponPower);

                    }
                    else
                    {
                        //피격 이펙트의 위치를 레이가 부딪힌 지점으로 이동시킴
                        bulletEffect.transform.position = hitInfo.point;
                        //피격 이팩트의 forward 방향을 레이가 부딪힌 지점의 법선 벡터와 일치시킨다.
                        bulletEffect.transform.forward = hitInfo.normal;
                        ps.Play();
                    }
                }
                StartCoroutine(ShootEffectOn(0.05f));
                --now_bulletcount;
            }
            

        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) //숫자키 1을 눌렀다면 노말모드
        {
            wMode = WeponMode.Normal;
            Camera.main.fieldOfView = 60f;
            wModeText.text = "Normal Mode";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) //숫자키 2를 눌렀다면 스나이퍼모드
        {
            wMode = WeponMode.Sniper;
            wModeText.text = "Sniper Mode";
        }

        nowBulletText.text = "총알 개수 : " + now_bulletcount;
        nowBombText.text = "수류탄 개수 : " + now_bombcount;
    }

    IEnumerator ShootEffectOn(float duration)
    {
        //랜덤하게 숫자를 0~4까지 숫자를 뽑는다.
        int num = Random.RandomRange(0, eff_Flash.Length);
        eff_Flash[num].SetActive(true);
        yield return new WaitForSeconds(duration);
        eff_Flash[num].SetActive(false);
    }
}
