using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class UM_DeadState : FSM_State
{
    [NonSerialized]
    public UnitMeleeControl parent;
    public override void Enter()
    {
        base.Enter();
        parent.dataBinding.Dead = true;
    }
    public override void OnAnimExit()
    {
        //base.OnAnimExit();
    }
    public override void OnAnimMiddle()
    {
        parent.OnDead();
    }
}
