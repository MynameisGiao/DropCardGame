using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionView : BaseView
{
    public GameObject stage;
    public GameObject btn_back;
    public List<Button> level =new List<Button>();
    public int cur_mission;
    public static int mission_choose;
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        stage.SetActive(true);
        btn_back.SetActive(true);
        cur_mission = DataController.instance.GetCurMissionData();
        for( int i=0; i< cur_mission; i++)
        {
            level[i].interactable = true;
        }
    }
    public void ShowHomeView()
    {
        SoundManager.instance.OnPlayButtonSound();
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
   
    public void OnPlayMission(int id)
    {
        SoundManager.instance.OnPlayButtonSound();
        stage.SetActive(false);
        btn_back.SetActive(false);
        ConfigMissionRecord cf_mission =ConfigManager.instance.configMission.GetRecordByKeySearch(id);
        mission_choose = cf_mission.ID;
        GameManager.instance.cur_cf_mission = cf_mission;
        Debug.LogError("Scene name: " + cf_mission.SceneName);
        LoadSceneManager.instance.LoadSceneByName(cf_mission.SceneName, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.IngameView);
        });
    }
}
