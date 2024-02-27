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

        //LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        //{
        //    Debug.LogError("load scene by index done!");
        //});
    }
    public void LoadSceneDone()
    {
        Debug.LogError("Load Scene Done!");
    }
}
