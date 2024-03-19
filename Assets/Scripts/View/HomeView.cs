using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeView : BaseView
{
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        DataController.instance.CreateMissionData();
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
