using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinDialog : BaseDialog
{
    public TMP_Text coin_lb;
    public TMP_Text gem_lb;
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

    public override void Setup(DialogParam param)
    {
        base.Setup(param);
        WinDialogParam dl_param=(WinDialogParam)param;
        coin_lb.text = dl_param.cf_mission.Reward_1.ToString();
        gem_lb.text = dl_param.cf_mission.Reward_2.ToString();
        Debug.LogError("Mission: " + dl_param.cf_mission.ID);
    }
    public void OnClaim()
    {
        DialogManager.instance.HideDialog(dialogIndex);
        ViewManager.instance.SwitchView(ViewIndex.EmptyView);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.MissionView);
        });

    }
}
