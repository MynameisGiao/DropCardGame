using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class ConfigMissionRecord
{
    // id	stage	name	sceneName	waves	reward

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
    private int stage;
    public int Stage
    {
        get
        {
            return stage;
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
    private string sceneName;
    public string SceneName
    {
        get
        {
            return sceneName;
        }
    }
    [SerializeField]
    private string waves;
     public string Waves
    {
        get
        {
            return waves;
        }
    }
    [SerializeField]
    private int reward_1;
    public int Reward_1
    {
        get
        {
            return reward_1;
        }
    }
    [SerializeField]
    private int reward_2;
    public int Reward_2
    {
        get
        {
            return reward_2;
        }
    }
}
public class ConfigMission : BYDataTable<ConfigMissionRecord>
{
    public override ConfigCompare<ConfigMissionRecord> DefindCompare()
    {
        configCompare=new ConfigCompare<ConfigMissionRecord>("id","stage");
        return configCompare;
    }
}
   
