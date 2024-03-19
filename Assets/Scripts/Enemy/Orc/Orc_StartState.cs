using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Orc_StartState : FSM_State
{
    [NonSerialized]
    public OrcControl parent;
    public override void Enter()
    {
        base.Enter();
        parent.dataBinding.StartStates = true;

    }
    public override void OnAnimMiddle()
    {
        base.OnAnimMiddle();
        parent.GotoState(parent.moveState);
    }

}