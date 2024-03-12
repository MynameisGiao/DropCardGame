using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionView : BaseView
{
    public override void Setup(ViewParam param)
    {
        base.Setup(param);

    }
    public void ShowHomeView()
    {
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
    public void OnPlayMission(int id)
    {
        ConfigMissionRecord cf_mission =ConfigManager.instance.configMission.GetRecordByKeySearch(id);
        GameManager.instance.cur_cf_mission = cf_mission;
        Debug.LogError("Scene name: " + cf_mission.SceneName);
        LoadSceneManager.instance.LoadSceneByName(cf_mission.SceneName, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.IngameView);
        });
    }
}
