using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConfigUnitLevelRecord
{
    // id	maxlv
    [SerializeField]
    private int id;
    public int ID
    {
        get
        {
            return id;
        }
    }
    [SerializeField]
    private int maxlv;
    public int Maxlv
    {
        get
        {
            return maxlv;
        }
    }

    // min_cost max_cost    factor_cost
    [SerializeField]
    private int min_cost;
    //public int Min_cost
    //{
    //    get
    //    {
    //        return min_cost;
    //    }
    //}

    [SerializeField]
    private int max_cost;
  
    [SerializeField]
    private float factor_cost;
    public int GetCost(int level)
    {
        return Utilities.CalculatorStat(min_cost, max_cost, maxlv, level, factor_cost);
    }
    //	min_range	max_range	factor_range
    [SerializeField]
    private int min_range;

    [SerializeField]
    private int max_range;
  
    [SerializeField]
    private float factor_range; 

    public int GetRange(int level)
    {
        return Utilities.CalculatorStat(min_range, max_range, maxlv, level, factor_range);
    }
    // min_attack_speed	max_attack_speed	factor_attack_speed	
    [SerializeField]
    private float min_attack_speed;
    
    [SerializeField]
    private float max_attack_speed;
 
    [SerializeField]
    private float factor_attack_speed;
 
    public float GetAttackSpeed(int level)
    {
        return Utilities.CalculatorStat(min_attack_speed, max_attack_speed, maxlv, level, factor_attack_speed);
    }
    //min_damage	max_damage	factor_damage	
    [SerializeField]
    private int min_damage;
    
    [SerializeField]
    private int max_damage;
   
    [SerializeField]
    private float factor_damage;
 
    public int GetDamage(int level)
    {
        return Utilities.CalculatorStat(min_damage, max_damage, maxlv, level, factor_damage);
    }
    // min_power_skill	max_power_skill	factor_power_skill
    [SerializeField]	
    private int min_power_skill;

    [SerializeField]
    private int max_power_skill;

    [SerializeField]
    private float factor_power_skill;
 
    public int GetPowerSkill(int level)
    {
        return Utilities.CalculatorStat(min_power_skill, max_power_skill, maxlv, level, factor_power_skill);
    }
    //min_hp	max_hp	factor_hp
    [SerializeField]
    private int min_hp;
   

    [SerializeField]
    private int max_hp;
 
    [SerializeField]
    private float factor_hp;
   
    public int GetHP(int level)
    {
        return Utilities.CalculatorStat(min_hp, max_hp, maxlv, level, factor_hp);
    }
}
public class ConfigUnitLevel : BYDataTable<ConfigUnitLevelRecord>
{
    public override ConfigCompare<ConfigUnitLevelRecord> DefindCompare()
    {
        configCompare = new ConfigCompare<ConfigUnitLevelRecord>("id");
        return configCompare;
    }
}
