using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeView : BaseView
{
    public Image notice_reward;
    private int cur_mission;
    private int cur_reward;
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        cur_mission = DataController.instance.GetCurMissionData();
        cur_reward = DataController.instance.GetCurReward();
        if (cur_mission - 1 > cur_reward)
            notice_reward.gameObject.SetActive(true);
        else
            notice_reward.gameObject.SetActive(false);
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
   
    public void ShowRewardView()
    {
        SoundManager.instance.OnPlayButtonSound();
        ViewManager.instance.SwitchView(ViewIndex.RewardView);

    }

}
