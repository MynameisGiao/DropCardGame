using System;
using System.Collections.Generic;

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

    public PlayerMissionData GetMissionData()
    {
        PlayerMissionData mission_data= dataModel.ReadData<PlayerMissionData>(DataSchema.MISSION_DATA);
        return mission_data;
    }
    public int GetCurMissionData()
    {
        return dataModel.ReadData<int>(DataSchema.CUR_MISSION);
    }public int GetCurReward()
    {
        return dataModel.ReadData<int>(DataSchema.REWARD);
    }
    public void UnpdateCurMissonData(int cur_data)
    {
        PlayerMissionData missionData = GetMissionData();
        missionData.cur_mission= cur_data;
        dataModel.UpdateData(DataSchema.MISSION_DATA, missionData);
    }public void UnpdateCurReward(int cur_reward)
    {
        PlayerInfo playerInfo = GetPlayerInfo();
        playerInfo.reward= cur_reward;
        dataModel.UpdateData(DataSchema.INFO, playerInfo);
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
    public void UpdateUnitLevel(int id)
    {
        UnitData unit = dataModel.ReadDicData<UnitData>(DataSchema.DIC_UNIT, id.Tokey());
        unit.level++;
        dataModel.UpdateDicData<UnitData>(DataSchema.DIC_UNIT, id.Tokey(), unit);

    }
    public UnitData GetUnitData(int id)
    {
        return dataModel.ReadDicData<UnitData>(DataSchema.DIC_UNIT, id.Tokey());
    }

    public void UnlockUnit(ConfigUnitLevelRecord configUnitLevelRecord, Action callback)
    {
        UnitData unitData = GetUnitData(configUnitLevelRecord.ID);
        if (unitData == null)
        {
            unitData = new UnitData();
            unitData.id = configUnitLevelRecord.ID;
            unitData.level = 1;
            int gold = GetGold();
            int min_cost = configUnitLevelRecord.GetCost(1);
            if (gold >= min_cost)
            {
                gold -= min_cost;
                dataModel.UpdateData(DataSchema.GOLD, gold);
                dataModel.UpdateDicData<UnitData>(DataSchema.DIC_UNIT, unitData.id.Tokey(), unitData);
                dataModel.UpdateDicData<UnitData>(DataSchema.DIC_UNIT_EX_DECK, unitData.id.Tokey(), unitData);

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
                int costlevel_next = cf_unit_lv.GetCost(unitData.level + 1);
                int gold = GetGold();
                if (gold >= costlevel_next)
                {
                    unitData.level = unitData.level + 1;

                    gold -= costlevel_next;
                    dataModel.UpdateData(DataSchema.GOLD, gold);
                    dataModel.UpdateDicData<UnitData>(DataSchema.DIC_UNIT, unitData.id.Tokey(), unitData);
                    
                    // Nếu unit nằm trong deck, cập nhật lại deck; nếu không, cập nhật DIC_UNIT_EX_DECK
                    List<UnitData> deck = GetDeck();
                    bool isInDeck = deck.Exists(u => u.id == unitData.id);
                    if (!isInDeck)
                    {
                        dataModel.UpdateDicData<UnitData>(DataSchema.DIC_UNIT_EX_DECK, unitData.id.Tokey(), unitData);
                    }
                    else
                    {
                        // Unit nằm trong deck, cập nhật lại deck
                        for (int i = 0; i < deck.Count; i++)
                        {
                            if (deck[i].id == unitData.id)
                            {
                                deck[i] = unitData;
                                break;
                            }
                        }
                        dataModel.UpdateData(DataSchema.DECK, deck);
                    }
                }
            }

        }
        callback();
    }

    
    public List<UnitData> GetDeck()
    {
        return dataModel.ReadData<List<UnitData>>(DataSchema.DECK);
    }

    public void ChangeDeck(UnitData unitData, int index)
    {
        List<UnitData> deck = dataModel.ReadData<List<UnitData>>(DataSchema.DECK);
        Dictionary<string, UnitData> dic_unit_ex_deck = dataModel.ReadData<Dictionary<string, UnitData>>(DataSchema.DIC_UNIT_EX_DECK);
        
        // Lấy unit cũ đang ở vị trí này
        UnitData oldUnitData = deck[index];
        
        // Thay unit mới vào deck
        deck[index] = unitData;
        dataModel.UpdateData(DataSchema.DECK, deck);
        
        // Cập nhật dic_unit_ex_deck
        // Thêm unit cũ vào (vì nó ra khỏi deck)
        dic_unit_ex_deck[oldUnitData.id.Tokey()] = oldUnitData;
        
        // Xóa unit mới khỏi (vì nó vào deck rồi)
        if (dic_unit_ex_deck.ContainsKey(unitData.id.Tokey()))
        {
            dic_unit_ex_deck.Remove(unitData.id.Tokey());
        }
        
        dataModel.UpdateData(DataSchema.DIC_UNIT_EX_DECK, dic_unit_ex_deck);
    }
    public List<UnitData> GetAllUnlockedUnits()
    {
        Dictionary<string, UnitData> dic_units = dataModel.ReadData<Dictionary<string, UnitData>>(DataSchema.DIC_UNIT);
        List<UnitData> unlocked_units = new List<UnitData>(dic_units.Values);
        return unlocked_units;
    }
    public List<UnitData> GetUnlockedUnitsExcludeDeck()
    {
        Dictionary<string, UnitData> dic_units = dataModel.ReadData<Dictionary<string, UnitData>>(DataSchema.DIC_UNIT_EX_DECK);
        List<UnitData> unlocked_units = new List<UnitData>(dic_units.Values);
        return unlocked_units;
    }


}
