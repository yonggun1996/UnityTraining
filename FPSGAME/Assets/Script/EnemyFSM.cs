using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState
    {
        Idle, Move, Attack, Return, Damage, Die
    }
    EnemyState m_State;
    public float findDistance = 8f;
    Transform player;
    public float attackDistance = 2f;
    public float moveSpeed = 5f;
    CharacterController cc;

    float currentTime = 0;
    float attackDelay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        m_State = EnemyState.Idle;
        player = GameObject.Find("Player").transform;
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case EnemyState.Idle: Idle(); break;
            case EnemyState.Move: Move(); break;
            case EnemyState.Attack: Attack(); break;
            /*case EnemyState.Return: Return(); break;
            case EnemyState.Damage: Damage(); break;
            case EnemyState.Die: Die(); break;*/
        }
    }

    void Idle()
    {
        
        if(Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("상태 전환: Idle -> Move");
        }
    }

    void Move()
    {
        if(Vector3.Distance(transform.position, player.position) > attackDistance)//플레이어와의 거리가 공격범위 밖이라면 플레이어에게 다가간다
        {
            Vector3 dir = (player.position - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }else//그렇지 않으면 공격상태로 전환
        {
            m_State = EnemyState.Attack;
            print("상태전환 : Move -> Attack");
            currentTime = attackDelay;
        }
    }

    void Attack()
    {
        //플레이어가 공격범위 이내에 있다면
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                print("공격상태");
                currentTime = 0;
            }
        }else//그렇지 않다면, 현재상태를 Move로 전환(다시 추격한다)
        {
            m_State = EnemyState.Move;
            print("상태전환 : Attack -> Move");
            currentTime = 0;
        }
    }
}
