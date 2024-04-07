using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using DG.DemiLib.External;
using UnityEngine;

public class DeckUIControl : MonoBehaviour
{
    public List<DeckItemUIControl> deck_items;
    private void OnEnable()
    {
        DataTrigger.RegisterValueChange(DataSchema.DECK,DeckDataChange);
        DataTrigger.RegisterValueChange(DataSchema.DIC_UNIT,DeckDataChange);
    }
    private void OnDisable()
    {
        DataTrigger.UnRegisterValueChange(DataSchema.DECK, DeckDataChange);
        DataTrigger.RegisterValueChange(DataSchema.DIC_UNIT, DeckDataChange);
    }
    private void DeckDataChange(object data)
    {
        Setup();
    }
    public void Setup()
    {
        List<UnitData> decks = DataController.instance.GetDeck();
        for(int i = 0;i<decks.Count;i++)
        {
            deck_items[i].Setup(decks[i]);
        }
    }
}
