using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent1 : MonoBehaviour
{
    public EnemyFSM2 efsm;

    public void PlayerHit()
    {
        efsm.AttackAction();
    }
}
