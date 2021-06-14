using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent2 : MonoBehaviour
{
    public EnemyFSM3 efsm;

    public void PlayerHit()
    {
        efsm.AttackAction();
    }
}
