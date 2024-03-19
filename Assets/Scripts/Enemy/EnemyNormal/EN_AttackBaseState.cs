using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class EN_AttackBaseState : FSM_State
{
    [NonSerialized]
    public EnemyNormalControl parent;
    public override void Enter()
    {
        base.Enter();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(parent.time_attack >= parent.cf.Attack_rate)
        {
            parent.dataBinding.Attack = true;
            parent.time_attack = 0;
        }
    }
    public override void OnAnimMiddle()
    {
        base.OnAnimMiddle();
        Debug.LogError("Attack base!");

        // base chịu damage
        MissionManager.instance.OnDamage(parent.damageData);
    }
}
