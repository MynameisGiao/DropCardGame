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
        SoundManager.instance.OnPlayButtonSound();
        ViewManager.instance.SwitchView(ViewIndex.MissionView);

    }
    public void ShowDeckView()
    {
        SoundManager.instance.OnPlayButtonSound();
        ViewManager.instance.SwitchView(ViewIndex.DeckView);
    }
    public void ShowShopView()
    {
        SoundManager.instance.OnPlayButtonSound();
        ViewManager.instance.SwitchView(ViewIndex.ShopView);

    }
    public void ShowDailyDialog()
    {
        SoundManager.instance.OnPlayButtonSound();
        DialogManager.instance.ShowDialog(DialogIndex.DailyDialog);

    }public void ShowRewardDialog()
    {
        SoundManager.instance.OnPlayButtonSound();
        DialogManager.instance.ShowDialog(DialogIndex.RewardDialog);

    }

}
