using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinDialog : BaseDialog
{
    public TMP_Text coin_lb;
    public TMP_Text gem_lb;
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
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.MissionView);
        });

    }
}
