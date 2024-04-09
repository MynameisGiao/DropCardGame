using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyDialog : BaseDialog
{
    public int lastDate;

    public int day_1;
    public GameObject off_1;
    public GameObject active_1;
    public GameObject check_1;

    public int day_2;
    public GameObject off_2;
    public GameObject active_2;
    public GameObject check_2;

    private void Start()
    {
        day_1 = PlayerPrefs.GetInt("day_1");
        day_2 = PlayerPrefs.GetInt("day_2");
        lastDate = PlayerPrefs.GetInt("lastDate");

        Reward();

        if(lastDate != System.DateTime.Now.Day)
        {
            if(day_1==0)
                day_1 = 1;
            else if (day_2 == 0)
                day_2 = 1;
        }
    }

    public void Reward()
    {
        if(day_1 == 0)
        {
            off_1.SetActive(true);
            active_1.SetActive(false);
            check_1.SetActive(false);
        }
        if (day_1 == 1)
        {
            active_1.SetActive(true);
            off_1.SetActive(false);
            check_1.SetActive(false);
        }
        if (day_1 == 2)
        {
            check_1.SetActive(true);
            off_1.SetActive(false);
            active_1.SetActive(false);
        }

        if (day_2 == 0)
        {
            off_2.SetActive(true);
            active_2.SetActive(false);
            check_2.SetActive(false);
        }
        if (day_2 == 1)
        {
            active_2.SetActive(true);
            off_2.SetActive(false);
            check_2.SetActive(false);
        }
        if (day_2 == 2)
        {
            check_2.SetActive(true);
            off_2.SetActive(false);
            active_2.SetActive(false);
        }
    }
    
    public void GetReward_1()
    {
        lastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("lastDate", lastDate);

        print("Reward1");

        day_1 = 2;
        PlayerPrefs.SetInt("day_1", 2);

        Reward();
    }
    public void GetReward_2()
    {
        lastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("lastDate", lastDate);

        print("Reward2");

        day_2 = 2;
        PlayerPrefs.SetInt("day_2", 2);

        Reward();
    }
    public void OnClose()
    {
        DialogManager.instance.HideDialog(dialogIndex);
    }
}
