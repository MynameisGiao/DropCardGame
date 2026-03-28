using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckStorageControl : MonoBehaviour
{
    public Transform parent_item;
    public DeckItemStorage prefab;
 
    private List<DeckItemStorage> items= new List<DeckItemStorage>();

    private void OnEnable()
    {
        DataTrigger.RegisterValueChange(DataSchema.DECK, DeckDataChange);
        DataTrigger.RegisterValueChange(DataSchema.DIC_UNIT, DeckDataChange);
    }
    private void OnDisable()
    {
        DataTrigger.UnRegisterValueChange(DataSchema.DECK, DeckDataChange);
        DataTrigger.UnRegisterValueChange(DataSchema.DIC_UNIT, DeckDataChange);
    }
    private void DeckDataChange(object data)
    {
        Setup();
    }
    public void Setup()
    {
        List<ConfigUnitRecord> lsUnit_cf= ConfigManager.instance.configUnit.GetUnitConfigCollection();
        if(items.Count <= 0)
        {
            for(int i=0; i<lsUnit_cf.Count; i++)
            {
                DeckItemStorage item= Instantiate(prefab);
                item.transform.SetParent(parent_item,false);
                items.Add(item);
            }
        }
        for(int i=0; i<lsUnit_cf.Count;i++)
        {
            items[i].Setup(lsUnit_cf[i]);
        }
    }
    void Update()
    {

    }
}
