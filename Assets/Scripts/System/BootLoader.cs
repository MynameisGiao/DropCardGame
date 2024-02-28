using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootLoader : MonoBehaviour
{
    IEnumerator Start()
    {
        DontDestroyOnLoad(gameObject);
        yield return new WaitForSeconds(1);
        LoadSceneManager.instance.LoadSceneByName("Buffer", LoadSceneDone);

    }
    public void LoadSceneDone()
    {
        Debug.LogError("Load Scene Done!");
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
}
