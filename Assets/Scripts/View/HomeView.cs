using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeView : BaseView
{
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        ConfigEnemyRecord cf_mission = ConfigManager.instance.configEnemy.records[0];
        Debug.LogError("Homeview " + cf_mission.Name);

    }
    public void ShowMissionView()
    {
        ViewManager.instance.SwitchView(ViewIndex.MissionView);
    }
    public void ShowDeckView()
    {
        ViewManager.instance.SwitchView(ViewIndex.MissionView);
    }
    public void ShowShopView()
    {
        ViewManager.instance.SwitchView(ViewIndex.ShopView);

    }
}
