using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : BYSingletonMono<ConfigManager>
{
    public ConfigEnemy configEnemy;
    public ConfigMission configMission;
    public ConfigWave configWave;
    public ConfigShop configShop;
    public ConfigUnit configUnit;
    public ConfigUnitLevel configUnitLevel;
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

        configWave = Resources.Load("Config/ConfigWave", typeof(ScriptableObject)) as ConfigWave;
        yield return new WaitUntil(() => configWave != null);

        configShop = Resources.Load("Config/ConfigShop", typeof(ScriptableObject)) as ConfigShop;
        yield return new WaitUntil(() => configShop != null);

        configUnit = Resources.Load("Config/ConfigUnit", typeof(ScriptableObject)) as ConfigUnit;
        yield return new WaitUntil(() => configUnit != null);

        configUnitLevel = Resources.Load("Config/ConfigUnitLevel", typeof(ScriptableObject)) as ConfigUnitLevel;
        yield return new WaitUntil(() => configUnitLevel != null);

        if (callback != null)
        {
            callback();
        }
    }


}
