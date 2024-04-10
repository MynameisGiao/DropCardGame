using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardView : BaseView
{
    public List<TMP_Text> reward_lb;
    public List<Button> buttons;
    public List<Image> checks;
    private int cur_mission;
    private int cur_reward;

   
    public override void OnShowView()
    {
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
        base.OnShowView();
        cur_mission = DataController.instance.GetCurMissionData();
        cur_reward = DataController.instance.GetCurReward();
       
        if(WinDialog.check_done != true)
        {
            for (int i = 0; i < cur_mission - 1; i++)
            {
                buttons[i].interactable = true;

            }
        }
        else
        {
            for (int i = 0; i < cur_mission ; i++)
            {
                buttons[i].interactable = true;

            }
        }
        
        if (cur_reward > 0)
        {
            for (int j = 0; j < cur_reward; j++)
            {
                buttons[j].interactable = false;
                checks[j].gameObject.SetActive(true);
            }
               
        }
    }

    public void OnClaim(int id)
    {

        int rewardValue;
        if (int.TryParse(reward_lb[id - 1].text, out rewardValue))
        {
            DataController.instance.AddGold(rewardValue);
            cur_reward++;
            DataController.instance.UnpdateCurReward(cur_reward);
            buttons[id - 1].interactable = false;
            checks[id-1].gameObject.SetActive(true);
        }
    }

    public void OnClose()
    {
        SoundManager.instance.OnPlayButtonSound();
      ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
}
