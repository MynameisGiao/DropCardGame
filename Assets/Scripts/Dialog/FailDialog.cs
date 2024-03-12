using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailDialog : BaseDialog
{
    public override void OnShowDialog()
    {
        base.OnShowDialog();
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    public override void OnHideDialog()
    {
        base.OnHideDialog();
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
   
    public void OnBack()
    {
        DialogManager.instance.HideDialog(dialogIndex);
        ViewManager.instance.SwitchView(ViewIndex.EmptyView);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.MissionView);
        });
    }
    public void OnRestart()
    {
        
            DialogManager.instance.HideDialog(dialogIndex);
            LoadSceneManager.instance.ReloadCurrentScene();
            ViewManager.instance.SwitchView(ViewIndex.IngameView);
            MissionManager.instance.StartMission();
        

    }

}
