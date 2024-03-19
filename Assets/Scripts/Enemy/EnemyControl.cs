using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInitData
{
    public ConfigEnemyRecord cf;
}
public class DamageData
{
    public int damage;
}
public class EnemyControl : FSM_System
{
    public Transform trans;
    public Transform trans_detect;
    public float range_detect;
    public float range_attack;
    public NavMeshAgent agent;
    public ConfigEnemyRecord cf;
    public int hp;
    public float time_attack;
    public DamageData damageData = new DamageData();
    public LayerMask mask_unit;
    public virtual void Setup(EnemyInitData enemyInitData)
    {
        trans = transform;
        agent=gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.Warp(trans.position);
        cf=enemyInitData.cf;
        hp=cf.Hp;
        damageData.damage=cf.Damage; // damage enemy lấy từ file cf
    }

    
    public virtual void OnDamage(DamageData enemyOnDamageData)
    {
        // chịu damage từ Unit
    }
    public void OnDead()
    {
        MissionManager.instance.EnemyDead(this);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        time_attack += Time.deltaTime;
    }
}
