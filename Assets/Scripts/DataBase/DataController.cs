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
        DataTrigger.RegisterValueChange(DataSchema.DIC_UNIT, (dic) =>
        {

        });
    }
    public void CreateMissionData()
    {
        //if(dataModel.ReadMissionData() == null)
        //{
        //    dataModel.CreateDataMission();
        //}

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
    public void AddGold(int number)
    {
        int gold=GetGold();
        gold += number;
        if(gold<=0)
            gold = 0;
        dataModel.UpdateData(DataSchema.GOLD, gold);
    }
    public void PayGold(int number)
    {
        int gold = GetGold();
        gold -= number;
        if (gold <= 0)
            gold = 0;
        dataModel.UpdateData(DataSchema.GOLD, gold);
    }

    public void AddGem(int number)
    {
        int gem = GetGem();
        gem += number;
        if (gem <= 0)
            gem = 0;
        dataModel.UpdateData(DataSchema.GEM, gem);
    }
    public void PayGem(int number)
    {
        int gem = GetGem();
        gem -= number;
        if (gem <= 0)
            gem = 0;
        dataModel.UpdateData(DataSchema.GEM, gem);
    }
    public void UpdateUnitLevel(int id)
    {
        UnitData unit = dataModel.ReadDicData<UnitData>(DataSchema.DIC_UNIT,id.Tokey());
        unit.level++;
        dataModel.UpdateDicData<UnitData>(DataSchema.DIC_UNIT,id.Tokey(),unit);
    }

    public void OnShopBuy(ConfigShopRecord cf)
    {
        if(cf.Shop_type==1) // add gold
        {
            PayGem(cf.Price);
            AddGold(cf.Value);
        }
        else if(cf.Shop_type == 2)// add gem
        {
            PayGold(cf.Price);
            AddGem(cf.Value);
        }
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
    public void FirstNameDialog()
    {
        
        DialogManager.instance.ShowDialog(DialogIndex.RenameDialog);
    }
    
}
