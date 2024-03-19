using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

public class EN_DeadState : FSM_State
{
    [NonSerialized]
    public EnemyNormalControl parent;
}