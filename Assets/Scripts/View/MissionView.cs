using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionView : BaseView
{
    public GameObject stage;
    public GameObject btn_back;
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        stage.SetActive(true);
        btn_back.SetActive(true);

    }
    public void ShowHomeView()
    {
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
    //public override void OnHideView()
    //{
    //    base.OnHideView();
    //}
    public void OnPlayMission(int id)
    {   
        stage.SetActive(false);
        btn_back.SetActive(false);
        ConfigMissionRecord cf_mission =ConfigManager.instance.configMission.GetRecordByKeySearch(id);
        GameManager.instance.cur_cf_mission = cf_mission;
        Debug.LogError("Scene name: " + cf_mission.SceneName);
        LoadSceneManager.instance.LoadSceneByName(cf_mission.SceneName, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.IngameView);
        });
    }
}
