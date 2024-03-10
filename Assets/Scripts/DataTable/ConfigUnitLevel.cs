using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConfigUnitLevelRecord
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
public class ConfigUnitLevel : BYDataTable<ConfigUnitLevelRecord>
{
    public override ConfigCompare<ConfigUnitLevelRecord> DefindCompare()
    {
        configCompare = new ConfigCompare<ConfigUnitLevelRecord>("id");
        return configCompare;
    }
}
