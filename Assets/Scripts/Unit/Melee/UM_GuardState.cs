using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UI;
[Serializable]
public class UM_GuardState : FSM_State
{ 
 [NonSerialized]
    public UnitMeleeControl parent;
    public float speed;
    private float cur_speed_anim;
    private Coroutine coroutine_;
    private Coroutine coroutine_dt;

    public override void Enter()
    {
        base.Enter();
        parent.u_agent.Warp(parent.trans.position);
        parent.u_agent.isStopped = false;
        parent.u_agent.speed = speed;
        cur_speed_anim = 0;
        if (coroutine_ != null)
            parent.StopCoroutine(coroutine_);
        coroutine_ = parent.StartCoroutine(LoopMove());
        if (coroutine_dt != null)
            parent.StopCoroutine(coroutine_dt);
        coroutine_dt = parent.StartCoroutine(LoopDetectEnemy());
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector3 dir = parent.u_agent.steeringTarget - parent.trans.position;
        float speed_anim = parent.u_agent.velocity.magnitude / parent.u_agent.speed;
        cur_speed_anim = Mathf.Lerp(cur_speed_anim, speed_anim * speed, Time.deltaTime * 5);
        dir.Normalize();
        if (dir.magnitude > 0)
        {
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            parent.trans.rotation = Quaternion.Slerp(parent.trans.rotation, q, Time.deltaTime * 120);
        }
       
        parent.dataBinding.Speed = cur_speed_anim;
    }
    IEnumerator LoopMove()
    {
        while (true)
        {
            Vector2 pos_random = UnityEngine.Random.insideUnitCircle * UnityEngine.Random.Range(1f, parent.range_detect);
            Vector3 pos_target = parent.pos_ogrinal + new Vector3(pos_random.x, 0, pos_random.y);
            parent.u_agent.SetDestination(pos_target);
            yield return new WaitForSeconds(0.3f);
            yield return new WaitUntil(() => parent.u_agent.remainingDistance <= 0.1f);
            yield return new WaitForSeconds(UnityEngine.Random.Range(1, 4));
        }

    }
    IEnumerator LoopDetectEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);
        while (true)
        {
            yield return wait;

            Collider[] cols = Physics.OverlapSphere(parent.trans.position, parent.range_detect, parent.mask_e);
            int index = -1;
            if (cols.Length == 1)
            {
                index = 0;

            }
            float distance = 100;
            for (int i = 0; i < cols.Length; i++)
            {
                float dis = Vector3.Distance(parent.trans.position, cols[i].transform.position);
                if (dis < distance)
                {
                    distance = dis;
                    index = i;
                }
            }

            if (index != -1)
            {
               parent.GotoState(parent.attackState, cols[index].transform);
            }

        }

    }
    public override void Exit()
    {
        base.Exit();
        parent.u_agent.isStopped = true;
        parent.dataBinding.Speed = 0;
        parent.u_agent.isStopped = true;
        if (coroutine_ != null)
            parent.StopCoroutine(coroutine_);
        coroutine_ = null;
        if (coroutine_dt != null)
            parent.StopCoroutine(coroutine_dt);
        coroutine_dt = null;
    }
}


