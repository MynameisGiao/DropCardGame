using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSchema
{
    public const string INFO = "info";
    public const string INVENTORY = "inventory";
    public const string GOLD = "inventory/gold";
    public const string GEM = "inventory/gem";
    public const string NAME = "info/nickname";
}
[Serializable]
public class PlayerData
{
    [SerializeField]
    public PlayerInfo info;
    [SerializeField]
    public PlayerInventory inventory;
    [SerializeField]
    public PlayerMissionData mission_data;

}
[Serializable]
public class PlayerInfo
{
    public string nickname;
    public int level;
    //public int exp;
    [SerializeField]
    public List<UnitData> deck=new List<UnitData>();
}
[Serializable]
public class PlayerInventory
{
    public int gold;
    public int gem;
    [SerializeField]
    public Dictionary<string,UnitData> dic_unit= new Dictionary<string, UnitData>();
}
[Serializable]
public class PlayerMissionData
{
    public int cur_mission;
    [SerializeField]
    public Dictionary<string,MissionData> dic_mission = new Dictionary<string,MissionData>();
}
[Serializable]
public class MissionData
{
    public int id;
}
[Serializable]
public class UnitData
{
    public int id;
    public int level;
}
