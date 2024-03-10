using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ConfigUnitRecord
{
    // id   name    prefab  damage  hp
    [SerializeField]
    private int id;
    [SerializeField]
    private string name;
    [SerializeField]
    private string prefab;
    [SerializeField]
    private int damage;
    [SerializeField]
    private int hp;
    [SerializeField]
    private int attack_rate;

}
public class ConfigUnit : BYDataTable<ConfigUnitRecord>
{
    public override ConfigCompare<ConfigUnitRecord> DefindCompare()
    {
        configCompare = new ConfigCompare<ConfigUnitRecord>("id");
        return configCompare;
    }
}
