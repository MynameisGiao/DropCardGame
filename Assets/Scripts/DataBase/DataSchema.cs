using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSchema
{
    public const string INFO = "info";
    public const string NAME = "info/nickname";
    public const string REWARD = "info/reward";
    public const string INVENTORY = "inventory";
    public const string GOLD = "inventory/gold";
    public const string GEM = "inventory/gem";   
    public const string DIC_UNIT = "inventory/dic_unit";
    public const string DECK = "info/deck";
    public const string MISSION_DATA = "mission_data";
    public const string CUR_MISSION = "mission_data/cur_mission";
  
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
    public int reward;
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
