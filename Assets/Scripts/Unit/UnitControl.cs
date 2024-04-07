using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Xml;
using UnityEngine;
using UnityEngine.AI;

public class UnitInitData
{
    public ConfigUnitRecord configUnit;
    public ConfigUnitLevelRecord configUnit_lv;
    public UnitData unitData;
   
}

public class UnitControl : FSM_System
{
    public Transform anchor_hub;
    public Transform trans;
    
    public NavMeshAgent u_agent;
    public Vector3 pos_ogrinal;
    public UnitInitData data;
    public int hp;
    private int max_hp;
    public float range_detect;
    public float range_attack;
    public float time_attack;
    public int damage;
    public LayerMask mask_e;

    private HPHub hpHub;
    public Transform anchorHub;
    // Start is called before the first frame update
    public virtual void Setup(UnitInitData unitInitData)
    {
        this.data = unitInitData;
        this.data.configUnit_lv = ConfigManager.instance.configUnitLevel.GetRecordByKeySearch(this.data.configUnit.ID);
        trans = transform;
        u_agent.updateRotation = false;
        pos_ogrinal = trans.position;
        hp = data.configUnit_lv.GetHP(data.unitData.level);
        max_hp = hp;
        range_detect = data.configUnit_lv.GetRange(data.unitData.level);
        damage= data.configUnit_lv.GetDamage(data.unitData.level);

        Transform hub_trans = BYPoolManager.instance.GetPool("HPHub").Spawn();
        IngameView ingameView = (IngameView)ViewManager.instance.cur_view;
        hub_trans.transform.SetParent(ingameView.parent_hub, false);
        hpHub = hub_trans.GetComponent<HPHub>();
        hpHub.Setup(anchor_hub, ingameView.parent_hub,Color.green);
    }

       
    public virtual void OnDamage(int damage_e)
    {
       hpHub.UpdateHP(hp, max_hp);
    }
    public void OnDead()
    {
        hpHub.OnDetachHub();
        Destroy(gameObject);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        time_attack += Time.deltaTime;
    }
}
