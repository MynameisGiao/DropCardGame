using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Orc_DeadState : FSM_State
{
    [NonSerialized]
    public OrcControl parent;
    public override void Enter()
    {
        base.Enter();
        parent.dataBinding.Dead = true;
    }
    public override void OnAnimExit()
    {
        base.OnAnimExit();
        parent.OnDead();
    }

}
