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
    public int Min_cost
    {
        get
        {
            return min_cost;
        }
    }
    [SerializeField]
    private int max_cost;
    public int Max_cost
    {
        get
        {
            return max_cost;
        }
    }
    [SerializeField]
    private float factor_cost;
    public float Factor_cost
    {
        get
        {
            return factor_cost;
        }
    }

    //	min_range	max_range	factor_range
    [SerializeField]
    private int min_range;
    public int Min_range
    {
        get
        {
            return min_range;
        }
    }
    [SerializeField]
    private int max_range;
    public int Max_range
    {
        get
        {
            return max_range;
        }
    }
    [SerializeField]
    private float factor_range;
    public float Factor_range
    {
        get
        {
            return factor_range;
        }
    }

    // min_attack_speed	max_attack_speed	factor_attack_speed	
    [SerializeField]
    private int min_attack_speed;
    public int Min_attack_speed
    {
        get
        {
            return min_attack_speed;
        }
    }
    [SerializeField]
    private int max_attack_speed;
    public int Max_attack_speed
    {
        get
        {
            return max_attack_speed;
        }
    }
    [SerializeField]
    private float factor_attack_speed;
    public float Factor_attack_speed
    {
        get
        {
            return factor_attack_speed;
        }
    }

    //min_damage	max_damage	factor_damage	
    [SerializeField]
    private int min_damage;
    public int Min_damage
    {
        get
        {
            return min_damage;
        }
    }
    [SerializeField]
    private int max_damage;
    public int Max_damage
    {
        get
        {
            return max_damage;
        }
    }
    [SerializeField]
    private float factor_damage;
    public float Factor_damage
    {
        get
        {
            return factor_damage;
        }
    }

    // min_power_skill	max_power_skill	factor_power_skill
     [SerializeField]	
    private int min_power_skill;
    public int Min_power_skill
    {
        get
        {
            return min_power_skill;
        }
    }
    [SerializeField]
    private int max_power_skill;
    public int Max_power_skill
    {
        get
        {
            return max_power_skill;
        }
    }
    [SerializeField]
    private float factor_power_skill;
    public float Factor_power_skill
    {
        get
        {
            return factor_power_skill;
        }
    }

    //min_hp	max_hp	factor_hp
    [SerializeField]
    private int min_hp;
    public int Min_hp
    {
        get
        {
            return min_hp;
        }
    }
    [SerializeField]
    private int max_hp;
    public int Max_hp
    {
        get
        {
            return max_hp;
        }
    }
    [SerializeField]
    private float factor_hp;
    public float Factor_hp
    {
        get
        {
            return factor_hp;
        }
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
