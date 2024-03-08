using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : BYSingletonMono<ConfigManager>
{
    public ConfigEnemy configEnemy;
    public ConfigMission configMission;
    public void InitConfig(Action callback)
    {
        StartCoroutine(ProgressLoadConfig(callback));
    }
    IEnumerator ProgressLoadConfig(Action callback)
    {
        configEnemy= Resources.Load("Config/ConfigEnemy",typeof(ScriptableObject)) as ConfigEnemy;
        yield return new WaitUntil(() => configEnemy != null);

        configMission = Resources.Load("Config/ConfigMission", typeof(ScriptableObject)) as ConfigMission;
        yield return new WaitUntil(() => configMission != null);

        if(callback != null)
        {
            callback();
        }
    }


}
