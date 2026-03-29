using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionControl : MonoBehaviour
{
    public Transform parent_item;
    public ItemCollection prefab;
 
    private List<ItemCollection> items= new List<ItemCollection>();

    public void SetupUnits(TypeCollection type)
    {
        switch (type)
        {
            case TypeCollection.All:
                List<ConfigUnitRecord> lsUnit_cf= ConfigManager.instance.configUnit.GetAllUnits();
                SetupUnitsFromList(lsUnit_cf);
                break;
            case TypeCollection.Legendary:
                List<ConfigUnitRecord> lsLegendary = ConfigManager.instance.configUnit.GetLegendaryUnits();
                SetupUnitsFromList(lsLegendary);
                break;
            case TypeCollection.Epic:
                List<ConfigUnitRecord> lsEpic = ConfigManager.instance.configUnit.GetEpicUnits();
                SetupUnitsFromList(lsEpic);
                break;
            case TypeCollection.Common:
                List<ConfigUnitRecord> lsCommon = ConfigManager.instance.configUnit.GetCommonUnits();
                SetupUnitsFromList(lsCommon);
                break;
        }
    }
    public void SetupUnitsFromList(List<ConfigUnitRecord> lsUnit_cf)
    {
        
        if(items.Count <= 0)
        {
            for(int i=0; i<lsUnit_cf.Count; i++)
            {
                ItemCollection item= Instantiate(prefab);
                item.transform.SetParent(parent_item,false);
                items.Add(item);
            }
        }
        
        // Setup only the items we need and disable the rest
        for(int i=0; i<items.Count; i++)
        {
            if(i < lsUnit_cf.Count)
            {
                items[i].gameObject.SetActive(true);
                items[i].Setup(lsUnit_cf[i]);
            }
            else
            {
                items[i].gameObject.SetActive(false);
            }
        }
    }
       
    void Update()
    {

    }
}
