using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameView : BaseView
{
    public Text wave_lb;
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        wave_lb.text = "WAVE: ";
    }
   
    public void OnPause()
    {
        DialogManager.instance.ShowDialog(DialogIndex.PauseDialog);
    }
   
    public override void OnShowView()
    {
        MissionManager.instance.OnWaveChange += OnWaveChange;
    }

    private void OnWaveChange(int arg1, int arg2)
    {
        Debug.LogError("WAVE: " + arg1.ToString() + " / " + arg2.ToString());
        wave_lb.text = "WAVE: " + arg1.ToString() + " / " + arg2.ToString();
    }

    public override void OnHideView()
    {
        MissionManager.instance.OnWaveChange -= OnWaveChange;

    }
}
