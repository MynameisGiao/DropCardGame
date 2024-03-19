using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

public class EN_MoveState : FSM_State
{
    [NonSerialized]
    public EnemyNormalControl parent;
    private Transform target;
    public float speed;
    private float cur_speed_anim;
    private float delay_check = 0;
    public override void Enter()
    {
        base.Enter();
        target = ConfigScene.instance.GetRandomTarget();
        parent.agent.isStopped = false;
        parent.agent.speed = speed;
        cur_speed_anim = 0;
        delay_check = 0;
        
    }
    public override void Update()
    {
        base.Update();
        delay_check += Time.deltaTime;
        parent.agent.SetDestination(target.position);
        UpdateRotation();
        float speed_anim= parent.agent.velocity.magnitude / parent.agent.speed;
        cur_speed_anim=Mathf.Lerp(cur_speed_anim,speed_anim * speed,Time.deltaTime*5);
        parent.dataBinding.Speed = cur_speed_anim;
        if(delay_check> 0.5f)
        {
            if (parent.agent.remainingDistance <= 0.1f)
            {
                parent.GotoState(parent.attackBaseState);
            }
        }
        
    }
    private void UpdateRotation()
    {
        Vector3 dir = parent.agent.steeringTarget - parent.trans.position;
        dir.Normalize();
        if(dir!= Vector3.zero)
        {
             Quaternion q= Quaternion.LookRotation(dir,Vector3.up);
            parent.trans.rotation = Quaternion.Slerp(parent.trans.rotation, q, Time.deltaTime *30);
        }
    }
    public override void Exit()
    {
        base.Exit();
        parent.agent.isStopped = true;
        parent.dataBinding.Speed=0;
    }
}