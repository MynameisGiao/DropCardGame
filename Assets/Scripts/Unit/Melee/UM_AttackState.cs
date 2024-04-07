using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
[Serializable]
public class UM_AttackState : FSM_State
{
    [NonSerialized]
    public UnitMeleeControl parent;
    private Transform trans_e;
    private EnemyControl enemy_control;
    private int damageData;
    public float speed;
    private float cur_speed_anim;
    private float attack_speed;
    private bool isAttacking;
    public override void Enter(object data)
    {

        trans_e = (Transform)data;
        enemy_control= trans_e.GetComponent<EnemyControl>();
        ConfigUnitLevelRecord cf_level = parent.data.configUnit_lv;
        int lv = parent.data.unitData.level;
        damageData = cf_level.GetDamage(lv);
        attack_speed=cf_level.GetAttackSpeed(lv);
        parent.u_agent.Warp(parent.trans.position);
        parent.u_agent.isStopped = false;
        parent.u_agent.speed = speed;
        cur_speed_anim = 0;
        isAttacking = false;
      
       
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (trans_e == null)
        {
            parent.GotoState(parent.guardState);
           
        }
        else if (enemy_control.hp <= 0)
        {
            parent.GotoState(parent.guardState);
        }
        else if (!isAttacking)
        {
            float dis = Vector3.Distance(parent.trans.position, trans_e.position);

            if (dis > parent.range_detect)
            {
                parent.GotoState(parent.guardState);
                return;
            }
            // tấn công enemy
            parent.u_agent.SetDestination(trans_e.position);
            RotateToEnemy();
            float speed_anim = 2f; //parent.agent.velocity.magnitude / parent.agent.speed;
            cur_speed_anim = Mathf.Lerp(cur_speed_anim, speed_anim * speed, Time.deltaTime * 5);

            if (dis <= parent.range_attack + 0.1f)
            {
                parent.dataBinding.Speed = 0;
                parent.u_agent.isStopped = true;

                if (parent.time_attack >= attack_speed)
                {
                    parent.dataBinding.Attack = true;
                    parent.time_attack = 0;
                }

            }
            else
            {
                parent.dataBinding.Speed = cur_speed_anim;
                parent.u_agent.isStopped = false;
            }
        }
            
    }
    private void RotateToEnemy()
    {
        Vector3 pos_e = trans_e.position;
        pos_e.y = parent.trans.position.y;
        Vector3 dir = pos_e - parent.trans.position;

        dir.Normalize();

        Quaternion q = Quaternion.LookRotation(dir, Vector3.up);

        parent.trans.rotation = q;
    }
    public override void OnAnimEnter()
    {
        base.OnAnimEnter();
        isAttacking = true;
    }
    public override void OnAnimMiddle()
    {
        base.OnAnimMiddle();
       enemy_control.OnDamage(damageData);
       
    }
    public override void OnAnimExit()
    {
        base.OnAnimExit();
        isAttacking = false;
       
    }
    public override void Exit()
    {
        isAttacking = false;
        parent.u_agent.isStopped = false;
        parent.u_agent.speed = speed;
    }
}