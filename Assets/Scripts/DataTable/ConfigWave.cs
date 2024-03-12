using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConfigWaveRecord
{
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
    private int time_delay;
    public int Time_Delay
    {
        get
        {
            return time_delay;
        }
    }
    [SerializeField]
    private string time_spawn;
    public List<int> Time_Spawns
    {
        get
        {
            string[] s = time_spawn.Split(';');
            List<int> ls = new List<int>();
            foreach (string e in s)
            {
                ls.Add(int.Parse(e));
            }
            return ls;
        }
    }
    [SerializeField]
    private string enemies;
    public List<int> Enemies
    {
        get
        {
            string[] s = enemies.Split(';');
            List<int> ls = new List<int>();
            foreach (string e in s)
            {
                ls.Add(int.Parse(e));
            }
            return ls;
        }
    }
}
public class ConfigWave : BYDataTable<ConfigWaveRecord>
{
    public override ConfigCompare<ConfigWaveRecord> DefindCompare()
    {
        configCompare = new ConfigCompare<ConfigWaveRecord>("id");
        return configCompare;
    }
}
