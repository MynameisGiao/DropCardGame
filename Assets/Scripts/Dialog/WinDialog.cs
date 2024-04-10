using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinDialog : BaseDialog
{
    public TMP_Text coin_lb;
    public TMP_Text gem_lb;
    private int mission_done;
    private int cur_mission;
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
        mission_done = MissionView.mission_choose;
        cur_mission = DataController.instance.GetCurMissionData();
        Debug.LogError("choose" + mission_done + "cur: " + cur_mission);
        base.Setup(param);
        WinDialogParam dl_param=(WinDialogParam)param;
        coin_lb.text = dl_param.cf_mission.Reward_1.ToString();
        gem_lb.text = dl_param.cf_mission.Reward_2.ToString();
        Debug.LogError("Mission: " + dl_param.cf_mission.ID);
    }
    public void OnClaim()
    {
        if (mission_done == cur_mission)
        {
            cur_mission++;
        }
        int coin = int.Parse(coin_lb.text);
        int gem = int.Parse(gem_lb.text);
        DataController.instance.AddGold(coin);
        DataController.instance.AddGem(gem);
        DataController.instance.UnpdateCurMissonData(cur_mission);
        DialogManager.instance.HideDialog(dialogIndex);
       
        ViewManager.instance.SwitchView(ViewIndex.EmptyView);
        MusicManager.instance.OnPlayMusic(MusicType.bg_1);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
           
            ViewManager.instance.SwitchView(ViewIndex.MissionView);
        });

    }
}
