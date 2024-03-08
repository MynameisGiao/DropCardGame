using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class LoadSceneManager : BYSingletonMono<LoadSceneManager>
{
    public GameObject ui_object;
    public Image image_progress;
    public TMP_Text progress_lb;
    public float time_delay = 0.05f;
    public void LoadSceneByName(string scene_name, Action callback)
    {
        StartCoroutine(LoadSceneByNameProgress(scene_name, callback));
    }
    IEnumerator LoadSceneByNameProgress(string scene_name, Action callback)
    {
        {
            ui_object.SetActive(true);
            // xử lý bất đồng bộ
            AsyncOperation async = SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Single);
            WaitForSeconds wait_s = new WaitForSeconds(time_delay);
            int count = 0;
            while (count <= 100)
            {
                yield return wait_s;
                count++;
                progress_lb.text = count.ToString() + "%";
                image_progress.fillAmount = (float)count / 100f;
            }
            while (!async.isDone)
            {
                yield return wait_s;
                progress_lb.text = ((int)(async.progress * 100)).ToString() + "%";
                image_progress.fillAmount = async.progress;
            }
        }
        callback?.Invoke();
        ui_object.SetActive(false);
    }

    public void LoadSceneByIndex(int scene_index, Action callback)
    {
        StartCoroutine(LoadSceneByIndexProgress(scene_index, callback));
    }
    IEnumerator LoadSceneByIndexProgress(int scene_index, Action callback)
    {
        {
            ui_object.SetActive(true);
            // xử lý bất đồng bộ
            AsyncOperation async = SceneManager.LoadSceneAsync(scene_index, LoadSceneMode.Single);
            WaitForSeconds wait_s = new WaitForSeconds(time_delay);
            int count = 0;
            while (count <= 100)
            {
                yield return wait_s;
                count++;
                progress_lb.text = count.ToString() + "%";
                image_progress.fillAmount = (float)count / 100f;
            }
            while (!async.isDone)
            {
                yield return wait_s;
                progress_lb.text = ((int)(async.progress * 100)).ToString() + "%";
                image_progress.fillAmount = async.progress;
            }
        }
        callback?.Invoke();
        ui_object.SetActive(false);
    }
}
