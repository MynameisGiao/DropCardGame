using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CardRare
{
    COMMON=1,
    EPIC=2,
    LEGENDARY=3
}
[Serializable]
public class ConfigUnitRecord
{
    // id	name	prefab	rare	stamina	cool_down	skill

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
    private CardRare rare; // độ hiếm
    public CardRare Rare
    {
        get
        {
            return rare;
        }
    }
    [SerializeField]
    private int stamina;
    public int Stamina
    {
        get
        {
            return stamina;
        }
    }
    [SerializeField]
    private int cool_down;
    public int Cool_down
    {
        get
        {
            return cool_down;
        }
    }
    [SerializeField]
    private string skill;
    public string Skill
    {
        get
        {
            return skill;
        }
    }
}
public class ConfigUnit : BYDataTable<ConfigUnitRecord>
{
    public override ConfigCompare<ConfigUnitRecord> DefindCompare()
    {
        configCompare = new ConfigCompare<ConfigUnitRecord>("id");
        return configCompare;
    }
}
