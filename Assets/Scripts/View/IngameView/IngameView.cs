﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IngameView : BaseView
{
    public Text wave_lb;
    public List<DeckIngameItemControl> deck_items;
    public TMP_Text stamina_lb;
    public GameObject lock_ui;
    public RectTransform m_DraggingPlane;

    private int stamina;
    public UnityEvent<int> OnStanimaChange;
    public RectTransform parent_hub; 

    public Image hp_base_fg;


    public override void Setup(ViewParam param)
    {
        lock_ui.SetActive(false);
        OnStanimaChange.RemoveAllListeners();
        wave_lb.text = "WAVE: ";
        List<UnitData> decks = DataController.instance.GetDeck();
        for (int i = 0; i < decks.Count; i++)
        {
            deck_items[i].Setup(decks[i],this);
        }
        StopCoroutine("LoopStamina");
        stamina = 0;
        stamina_lb.text = stamina.ToString();

    }
   
    public void OnPause()
    {
        DialogManager.instance.ShowDialog(DialogIndex.PauseDialog);
    }
   
    public override void OnShowView()
    {
        hp_base_fg.fillAmount = 1;
        MissionManager.instance.OnWaveChange.AddListener(OnWaveChange);
        MissionManager.instance.OnBaseHpChange.AddListener(OnBaseHpChange);
        StartCoroutine("LoopStamina");

    }

    private void OnBaseHpChange(int hp, int maxhp)
    {
        float val = (float)hp / (float)maxhp;
        hp_base_fg.fillAmount = val;
    }


    private void OnWaveChange(int arg1, int arg2)
    {
        wave_lb.text = "WAVE: " + arg1.ToString() + "/" + arg2.ToString();
    }

    public override void OnHideView()
    {
        OnStanimaChange.RemoveAllListeners();
        StopCoroutine("LoopStamina");
    }
    IEnumerator LoopStamina()
    {
        WaitForSeconds wait = new WaitForSeconds(1.5f);
        while (true)
        {
            yield return wait;
            stamina++;
            stamina_lb.text = stamina.ToString();
            OnStanimaChange?.Invoke(stamina);
        }
            
       
    }
    public void OnDropUnit(UnitData unitData, ConfigUnitRecord configUnit)
    {
        if(stamina >= configUnit.Stamina)
            stamina -= configUnit.Stamina;
    }
}
