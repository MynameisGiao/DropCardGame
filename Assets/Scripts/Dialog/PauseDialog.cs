using System.Collections;
using System.Collections.Generic;
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
        DialogManager.instance.HideDialog(dialogIndex);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.MissionView);
        });
    }
    public void OnSettingDialog()
    {
        DialogManager.instance.HideDialog(dialogIndex);
        DialogManager.instance.ShowDialog(DialogIndex.SettingDialog, new SettingDialogParam { isShowPause = true });
       // DialogManager.instance.ShowDialog(DialogIndex.FailDialog);
    }
    public void OnRestart()
    {
        DialogManager.instance.HideDialog(dialogIndex);
        LoadSceneManager.instance.ReloadCurrentScene();
        ViewManager.instance.SwitchView(ViewIndex.IngameView);
        MissionManager.instance.StartMission();
    }
}
