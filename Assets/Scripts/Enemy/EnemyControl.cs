using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInitData
{
    public ConfigEnemyRecord cf;
}

public class EnemyControl : FSM_System
{
    public Transform anchor_hub;

    public Transform trans;
    public Transform trans_detect;
    public float range_detect;
    public float range_attack;
    public NavMeshAgent agent;
    public ConfigEnemyRecord cf;
    public int hp;
    public float time_attack;
    public int damage;
    public LayerMask mask_unit;
    public HPHub hp_hub;
    public virtual void Setup(EnemyInitData enemyInitData)
    {
        trans = transform;
        agent=gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.Warp(trans.position);
        cf=enemyInitData.cf;
        hp=cf.Hp;
        damage=cf.Damage;

        Transform hub_trans = BYPoolManager.instance.GetPool("HPHub").Spawn();
        IngameView ingameView= (IngameView)ViewManager.instance.cur_view;
        hub_trans.transform.SetParent(ingameView.parent_hub,false);
        hp_hub= hub_trans.GetComponent<HPHub>();
        hp_hub.SetUp(anchor_hub, ingameView.parent_hub, Color.red);

    }

    
    public virtual void OnDamage(int damage_u)
    {
        // chịu damage từ Unit
        hp_hub.UpdateHP(hp, cf.Hp);
    }
    public void OnDead()
    {
        hp_hub.OnDetachHub();
        MissionManager.instance.EnemyDead(this);
        Destroy(gameObject);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        time_attack += Time.deltaTime;
    }
}
