using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState
    {
        Idle, Move, Attack, Return, Damaged, Die
    }
    EnemyState m_State;
    public float findDistance = 8f;
    Transform player;
    public float attackDistance = 2f; //공격 가능 범위
    public float moveSpeed = 5f;
    CharacterController cc;

    float currentTime = 0;
    float attackDelay = 2f;

    public int attackPower = 3;
    public float moveDistanc = 20f;//이동 가능 범위
    Vector3 originPos;//초기 위치 저장할 변수
    Quaternion originRot;
    public int hp = 15;
    public int maxhp = 15;
    public Slider hpSlider;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        m_State = EnemyState.Idle;
        player = GameObject.Find("Player").transform;
        cc = GetComponent<CharacterController>();
        originPos = transform.position;
        originRot = transform.rotation;
        anim = transform.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case EnemyState.Idle: Idle(); break;
            case EnemyState.Move: Move(); break;
            case EnemyState.Attack: Attack(); break;
            case EnemyState.Return: Return(); break;
            //지속적 체크가 아니기 때문에 함수를 여기서 호출하지 않는다(죽는것과 피격은 필요한때만 부르기 때문)
            //그렇게 되면 DieProcess 함수가 호출이 안된다
            case EnemyState.Damaged: //Damaged(); 
                break;
            case EnemyState.Die: //Die(); 
                break;
                   
        }

        hpSlider.value = (float)hp / (float)maxhp;
    }
    void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("상태 전환: Idle -> Move");
            //이동애니메이션으로 전환하기
            anim.SetTrigger("IdleToMove");
        }
    }
    void Move()
    {
        //만일 현재 위치가 초기 위치에서 이동 가능 범위를 넘어간다면
        if (Vector3.Distance(transform.position, originPos) > moveDistanc)
        {
            m_State = EnemyState.Return;
            print("상태 전환 : Move->Return");
        }
        //플레이어와의 거리가 공격 범위 밖이라면(전역변수로 2m로 설정)
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime); // 플레이어에게 이동..
            transform.forward = dir;//이동방향대로 플레이어를 향해 움직이고 전환한다
        }
        else
        {
            m_State = EnemyState.Attack;
            print("상태전환: Move -> Attack");
            currentTime = attackDelay;
            anim.SetTrigger("MoveToAttackDelay");
        }
    }
    void Attack()
    {
        //플레이어가 공격범위 이내에 있다면
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                //player.GetComponent<PlayerMove>().DamageAction(attackPower);//PlayerMove 에 있는 attackPower를 인자값으로 받아오면 hp가 깍인다
                print("공격");
                currentTime = 0;
                anim.SetTrigger("StartAttack");
            }
        }
        else //그렇지 않다면, 현재 상태를 Move로 전환(재추격)
        {
            m_State = EnemyState.Move;
            print("상태전환 : Attack -> Move");
            currentTime = 0;
            anim.SetTrigger("AttackToMove");
        }
    }

    public void AttackAction()
    {
        player.GetComponent<PlayerMove>().DamageAction(attackPower);
    }

    void Return()
    {
        //초기위치에서의 거리가 0.1 이상이라면 초기 위치쪽으로 이동한다
        if(Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            transform.forward = dir;
        }else
        {
            //위치 값과 회전 값을 초기 상태로 변환한다.
            transform.position = originPos;
            transform.rotation = originRot;
            hp = maxhp;
            m_State = EnemyState.Idle;
            print("상태 전환: Return -> Idle");
            anim.SetTrigger("MoveToIdle");
        }
    }

    public void HitEnemy(int hitPower)
    {
        if(m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)//적군이 데미지를 입었거나, 죽었거나, 돌아가고 있는 상태면 깍지 말자
        {
            return; 
        }
        hp -= hitPower;//플레이어의 공격력만큼 에너미의 체력을 감소 시킨다
        print("Enemy의 체력 : " + hp);
        if(hp > 0)
        {
            m_State = EnemyState.Damaged;
            print("상태 전환 : Any state -> Damaged");
            Damaged();
        }else
        {//hp가 0 이하라면
            m_State = EnemyState.Die;
            print("상태 전환: Any state -> Die");
            Die();
        }
    }
    
    void Damaged()
    {
        StartCoroutine(DamageProcess());
    }

    IEnumerator DamageProcess()
    {
        //피격 모션 시간만큼 기다린다
        yield return new WaitForSeconds(0.5f);
        //현재 상태를 이동 상태로 전환한다
        m_State = EnemyState.Move;
        print("상태 전환 : Damaged->Move");
    }

    void Die()
    {
        //진행중인 코루틴을 멈추게 하는 함수
        StopAllCoroutines();
        StartCoroutine(DieProcess());
    }
    IEnumerator DieProcess()
    {
        cc.enabled = false;
        yield return new WaitForSeconds(2f);
        print("적 소멸");
        Destroy(gameObject);
    }

}
