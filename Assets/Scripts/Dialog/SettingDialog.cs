using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingDialog : BaseDialog
{
    private bool showPauseDialog = false;
    public override void OnShowDialog()
    {
        base.OnShowDialog();
        Time.timeScale = 0;
        if (showPauseDialog)
        {
            AudioListener.pause = true;
        }
    }
    public override void OnHideDialog()
    {
        base.OnHideDialog();
        Time.timeScale = 1;
        
    }
    public override void Setup(DialogParam param)
    {
        SettingDialogParam dialogParam = param as SettingDialogParam;
        if (dialogParam != null)
        {

            showPauseDialog = dialogParam.isShowPause;
        }
        base.Setup(param);
    }
    public void OnSetSound(float new_value)
    {
        SoundManager.instance.audioFx.volume = new_value;
    }
    public void OnSetMusic(float new_value)
    {
        MusicManager.instance.audioFx.volume = new_value;
    }
    public void OnClose()
    {

        DialogManager.instance.HideDialog(DialogIndex.SettingDialog);

        if (showPauseDialog)
        {
            DialogManager.instance.ShowDialog(DialogIndex.PauseDialog);
            showPauseDialog=false;
        }
    }
}
