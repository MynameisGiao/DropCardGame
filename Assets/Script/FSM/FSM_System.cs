using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_System : MonoBehaviour
{
    public FSM_State cur_State;
    private FSM_State previous_State;
    public void GotoState(FSM_State newState)
    {
        //  cur_State?.Exit();

        if (cur_State != null)
        {
            cur_State.Exit();
            previous_State = cur_State;
        }
        cur_State= newState;
        cur_State.Enter();
    }
    public void GotoState(FSM_State newState, object data)
    {
        if (cur_State != null)
        {
            cur_State.Exit();
            previous_State = cur_State;
        }
        cur_State = newState;
        cur_State.Enter(data);
    }
    public void GotoPreviousState()
    {
        
        if(previous_State!=null)
        {
            FSM_State temp_State = null;
            if (cur_State!=null)
            {
                cur_State.Exit();
                temp_State = cur_State;
            }
            cur_State= previous_State;
            cur_State.Enter();
            previous_State= temp_State;

        }
    }


    protected virtual void Update()
    {
        cur_State?.Update();
    }

    protected virtual void FixedUpdate()
    {
        cur_State?.FixedUpdate();
    }
    protected virtual void LateUpdate()
    {
        cur_State?.LateUpdate();
    }
    public void OnAnimEnter()
    {
        cur_State?.OnAnimEnter();
    }
    public void OnAnimMiddle()
    {
        cur_State?.OnAnimMiddle();
    }

    public void OnAnimExit()
    {
        cur_State?.OnAnimExit();
    }
}
