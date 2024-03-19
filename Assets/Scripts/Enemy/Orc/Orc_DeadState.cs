using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Orc_DeadState : FSM_State
{
    [NonSerialized]
    public OrcControl parent;
}
