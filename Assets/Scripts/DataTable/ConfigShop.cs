using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ConfigShopRecord
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
public class ConfigShop : BYDataTable<ConfigShopRecord>
{
    public override ConfigCompare<ConfigShopRecord> DefindCompare()
    {
        configCompare = new ConfigCompare<ConfigShopRecord>("id");
        return configCompare;
    }
}
