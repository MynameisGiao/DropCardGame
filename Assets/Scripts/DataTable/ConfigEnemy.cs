using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConfigEnemyRecord
{
    // id   name    prefab  damage  hp
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
    private string name;
    public string Name
    {
        get
        {
            return name;
        }
    }
    [SerializeField]
    private string prefab;
    public string Prefab
    {
        get
        {
            return prefab;
        }
    }
    [SerializeField]
    private int damage;
    public int Damage
    {
        get
        {
            return damage;
        }
    }
    [SerializeField]
    private int hp;
    public int Hp
    {
        get
        {
            return hp;
        }
    }
    [SerializeField]
    private int attack_rate;
    public int Attack_rate
    {
        get
        {
            return attack_rate;
        }
    }
}
public class ConfigEnemy : BYDataTable<ConfigEnemyRecord>
{
    public override ConfigCompare<ConfigEnemyRecord> DefindCompare()
    {
        configCompare = new ConfigCompare<ConfigEnemyRecord>("id");
        return configCompare;
    }
}
