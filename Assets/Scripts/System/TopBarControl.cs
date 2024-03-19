using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopBarControl : MonoBehaviour
{
    public RectTransform parent;
    public TMP_Text name_lb;
    public TMP_Text level_lb;
    public TMP_Text coin_lb;
    public TMP_Text gem_lb;
    
    void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded; 
    }

    private void SceneManager_sceneLoaded(UnityEngine.SceneManagement.Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.buildIndex==1)
        {
            PlayerInfo playerInfo = DataController.instance.GetPlayerInfo();
            name_lb.text =playerInfo.nickname;
            level_lb.text = playerInfo.level.ToString();
            gem_lb.text=DataController.instance.GetGem().ToString();
            coin_lb.text=DataController.instance.GetGold().ToString();
            parent.DOAnchorPosY(0, 1);
        }
        else
        {
            parent.DOAnchorPosY(500,1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowRenameDialog()
    {
        DialogManager.instance.ShowDialog(DialogIndex.RenameDialog);

    }
}
