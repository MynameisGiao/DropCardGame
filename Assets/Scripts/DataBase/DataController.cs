using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : BYSingletonMono<DataController>
{
    public DataModel dataModel;
    public void InitData(Action callback)
    {
        dataModel.InitData(callback);
    }
    public void CreateMissionData()
    {
        if(dataModel.ReadMissionData() == null)
        {
            dataModel.CreateDataMission();
        }

       
    }
    public PlayerInfo GetPlayerInfo()
    {
        PlayerInfo info = dataModel.ReadData<PlayerInfo>(DataSchema.INFO);
        return info;

    }
    public int GetGem()
    {
        return dataModel.ReadData<int>(DataSchema.GEM);
    }
    public int GetGold()
    {
        return dataModel.ReadData<int>(DataSchema.GOLD);
    }
    public string GetName()
    {
        return dataModel.ReadData<string>(DataSchema.NAME);
    }
    public void UpdateName(string newName)
    {
        PlayerInfo playerInfo = GetPlayerInfo();
        if (playerInfo != null)
        {
            playerInfo.nickname = newName;
            dataModel.UpdateData(DataSchema.INFO, playerInfo);
        }
    }
}
