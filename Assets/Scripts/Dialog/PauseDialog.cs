using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PauseDialog : BaseDialog
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
    
    public void OnContinue()
    {
        DialogManager.instance.HideDialog(dialogIndex);

    }
    public void OnBack()
    {
        MissionManager.instance.OnBaseHpChange.RemoveAllListeners();
        MissionManager.instance.OnWaveChange.RemoveAllListeners();
        BYPoolManager.instance.GetPool("HPHub").DeSpawnAll();
        DialogManager.instance.HideDialog(dialogIndex);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.MissionView);
        });
    }
    public void OnSettingDialog()
    {
        DialogManager.instance.ShowDialog(DialogIndex.SettingDialog, new SettingDialogParam { isShowPause = true });

    }
    public void OnRestart()
    {
        BYPoolManager.instance.GetPool("HPHub").DeSpawnAll();
        DialogManager.instance.HideDialog(dialogIndex);
        ViewManager.instance.SwitchView(ViewIndex.EmptyView);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.IngameView);
        });
        LoadSceneManager.instance.ReloadCurrentScene();
        MissionManager.instance.StartMission();
    }
}
