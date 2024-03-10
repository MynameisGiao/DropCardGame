using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ConfigWaveRecord
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
public class ConfigWave : BYDataTable<ConfigWaveRecord>
{
    public override ConfigCompare<ConfigWaveRecord> DefindCompare()
    {
        configCompare = new ConfigCompare<ConfigWaveRecord>("id");
        return configCompare;
    }
}
