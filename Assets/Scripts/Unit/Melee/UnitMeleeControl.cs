using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnitMeleeControl : UnitControl
{
    public UnitMeleeDataBinding dataBinding;
    public UM_GuardState guardState;
    public UM_AttackState attackState;
    public UM_DeadState deadState;
    public override void Setup(UnitInitData unitInitData)
    {
        base.Setup(unitInitData);
        guardState.parent = this;
        attackState.parent = this;
        deadState.parent = this;
        GotoState(guardState);
       
    }
    
    public override void OnDamage(int damage_e)
    {
        hp -= damage_e;
        if (hp <= 0)
        {
            if (cur_State != deadState)
            {
                GotoState(deadState);
            }
        }
       base.OnDamage(damage_e);
    }
}
