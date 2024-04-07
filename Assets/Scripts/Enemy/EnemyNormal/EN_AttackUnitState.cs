using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

public class EN_AttackUnitState   : FSM_State
{
    [NonSerialized]
    public EnemyNormalControl parent;
    public Transform target_unit;
    public float speed;
    private float cur_speed_anim;
    private float delay_check =0;
    private bool isAttacking;
    public override void Enter(object data)
    {
        base.Enter(data);
        target_unit=(Transform)data;
        parent.agent.isStopped = false;
        parent.agent.speed = speed;
        cur_speed_anim = 0;
        delay_check = 0;
        isAttacking = false;
        parent.agent.stoppingDistance = parent.range_attack;
    }

    public override void Update()
    {
        delay_check += Time.deltaTime;
        base.Update();

        if (target_unit == null)
        {
            parent.GotoState(parent.moveState);
            return;
        }

        if(!isAttacking)
        {
            // unit vượt tầm tấn công
            if(Vector3.Distance(parent.trans.position, target_unit.position) > parent.range_detect)
            {
                parent.GotoState(parent.moveState);
                return;
            }

            // tấn công unit
            parent.agent.SetDestination(target_unit.position);
            UpdateRotationTarget();
            float speed_anim = 2f; //parent.agent.velocity.magnitude / parent.agent.speed;
            cur_speed_anim = Mathf.Lerp(cur_speed_anim, speed_anim * speed, Time.deltaTime * 5);

            if (delay_check > 0.5f)
            {
                if (parent.agent.remainingDistance <= parent.range_attack + 0.1f)
                {
                    parent.dataBinding.Speed = 0;
                    if (parent.time_attack >= parent.cf.Attack_rate)
                    {
                        parent.dataBinding.Attack = true;
                        parent.agent.isStopped = true;
                        parent.time_attack = 0;
                    }

                }
                else
                {
                    parent.dataBinding.Speed = cur_speed_anim;
                }
            }
            else
            {
                parent.dataBinding.Speed = cur_speed_anim;
            }

        }

    }
    public override void OnAnimEnter()
    {
        base.OnAnimEnter();
        isAttacking = true;
    }
    public override void OnAnimMiddle()
    {
        base.OnAnimMiddle();
        if(Vector3.Distance(parent.trans.position, target_unit.position)<= parent.range_attack +0.1f)
        {
            target_unit.GetComponent<UnitControl>().OnDamage(parent.damage);
        }
       
    }
    public override void OnAnimExit()
    {
        base.OnAnimExit();
        parent.agent.isStopped = false;
        isAttacking = false;
    }
    private void UpdateRotationTarget()
    {
        Vector3 dir = parent.agent.steeringTarget - parent.trans.position;
        dir.Normalize();
        if (dir != Vector3.zero)
        {
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            parent.trans.rotation = Quaternion.Slerp(parent.trans.rotation, q, Time.deltaTime * 30);
        }
    }
    public override void Exit()
    {
        base.Exit();
        parent.agent.isStopped = false;
        isAttacking = false;
        parent.agent.stoppingDistance = 0;
    
    }
}