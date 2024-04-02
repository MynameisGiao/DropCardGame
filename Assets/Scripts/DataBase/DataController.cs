using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Newtonsoft.Json;
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
            int gem = GetGem();
            if (gem <= 0)
                return;
            PayGem(cf.Price);
            AddGold(cf.Value);
        }
        else if(cf.Shop_type == 2)// add gem
        {
            int gold = GetGold();
            if (gold <= 0)
                return;
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

    public List<UnitData> GetDeck()
    {
        return dataModel.ReadData<List<UnitData>>(DataSchema.DECK);
    }
    public UnitData GetUnitData(int id)
    {
        return dataModel.ReadDicData<UnitData>(DataSchema.DIC_UNIT, id.Tokey());
    }
    public void UnlockUnit(ConfigUnitLevelRecord configUnitLevelRecord, Action callback)
    {
        UnitData unitData=GetUnitData(configUnitLevelRecord.ID);
        if(unitData == null)
        {
            
            unitData = new UnitData();
            unitData.id = configUnitLevelRecord.ID;
            unitData.level = 1;
            int gold = GetGold();
            if(gold >= configUnitLevelRecord.Min_cost)
            {
                gold-=configUnitLevelRecord.Min_cost;
                dataModel.UpdateData(DataSchema.GOLD, gold);
                dataModel.UpdateDicData<UnitData>(DataSchema.DIC_UNIT, unitData.id.Tokey(), unitData);

            }
        }
        callback();
    }

    public void UpgradeUnit(ConfigUnitLevelRecord cf_unit_lv, Action callback)
    {
        UnitData unitData = GetUnitData(cf_unit_lv.ID);
        if (unitData != null)
        {
            if (unitData.level < cf_unit_lv.Maxlv)
            {
                int costLvNext = Utilities.CalculatorStat(cf_unit_lv.Min_cost, cf_unit_lv.Max_cost, cf_unit_lv.Maxlv, unitData.level + 1, cf_unit_lv.Factor_cost);
                int gold = GetGold();
                if (gold >= costLvNext)
                {
                    unitData.level = unitData.level + 1;
                    gold -= costLvNext;
                    dataModel.UpdateData(DataSchema.GOLD, gold);
                    dataModel.UpdateDicData<UnitData>(DataSchema.DIC_UNIT, unitData.id.Tokey(), unitData);

                }
                
            }
           
        }
        callback();
    }
    public void ChangDeck(UnitData unitData, int index)
    {
        List<UnitData> deck = dataModel.ReadData<List<UnitData>>(DataSchema.DECK);
        deck[index]= unitData;
        dataModel.UpdateData(DataSchema.DECK, deck);
    }

}
