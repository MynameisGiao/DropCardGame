using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingDialog : BaseDialog
{
    private bool showPauseDialog = false;

    public override void Setup(DialogParam param)
    {
        SettingDialogParam dialogParam = param as SettingDialogParam;
        if (dialogParam != null)
        {

            showPauseDialog = dialogParam.isShowPause;
        }
        base.Setup(param);
    }
    public void OnClose()
    {

        DialogManager.instance.HideDialog(DialogIndex.SettingDialog);

        if (showPauseDialog)
        {
            DialogManager.instance.ShowDialog(DialogIndex.PauseDialog);
        }
    }
}
