using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopBarControl : MonoBehaviour
{
    public RectTransform parent;
    public TMP_Text name_lb;
    public TMP_Text level_lb;
    public TMP_Text gold_lb;
    public TMP_Text gem_lb;

    private int gold, gem;
    private Tweener tween_goldGem;
   
    void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        ViewManager.instance.OnViewShow += ViewManager_OnViewShow;
        ViewManager.instance.OnViewHide += ViewManager_OnViewHide;
    }

    private void ViewManager_OnViewHide(BaseView obj)
    {
        if (obj.view_index == ViewIndex.HomeView)
        {
            //parent.DOAnchorPosY(500, 1);
        }
    }

    private void ViewManager_OnViewShow(BaseView obj)
    {
        parent.DOAnchorPosY(0, 0.5f);
        if (obj.view_index == ViewIndex.HomeView)
        {
            parent.DOAnchorPosY(0, 0.5f);
        }
        if (obj.view_index == ViewIndex.IngameView)
        {
            
            parent.DOAnchorPosY(500, 1);
        }
    }

    private void SceneManager_sceneLoaded(UnityEngine.SceneManagement.Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.buildIndex==1)
        {
            PlayerInfo playerInfo = DataController.instance.GetPlayerInfo();
            name_lb.text=playerInfo.nickname;
            level_lb.text = playerInfo.level.ToString();

            gold= DataController.instance.GetGold();
            gold_lb.text =gold.ToString();
            gem = DataController.instance.GetGem();
            gem_lb.text = gem.ToString();

            DataTrigger.RegisterValueChange(DataSchema.INVENTORY, DataGoldGemChange);
            DataTrigger.RegisterValueChange(DataSchema.INFO, DataNameChange);
            
        }
        
    }

    private void DataGoldGemChange(object data)
    {
        int new_gold = DataController.instance.GetGold();
        int new_gem = DataController.instance.GetGem();

        if (new_gold != gold || new_gem != gem)
        {
            if (tween_goldGem != null)
            {
                tween_goldGem.Kill();
            }

            tween_goldGem = DOTween.To(() => new Vector2(gold, gem), v => {
                gold = Mathf.RoundToInt(v.x);
                gem = Mathf.RoundToInt(v.y);
            }, new Vector2(new_gold, new_gem), 0.5f).OnUpdate(() =>
            {
                gold_lb.text = gold.ToString();
                gem_lb.text = gem.ToString();
            }).OnComplete(() =>
            {
                tween_goldGem = null;
            });

            tween_goldGem.Restart();
        }
    }

    public void AddGoldGem()
    {
        DialogManager.instance.HideAllDialog();
        if(ViewManager.instance.cur_view.view_index != ViewIndex.ShopView)
        {
            ViewManager.instance.SwitchView(ViewIndex.ShopView);
        }
    }
    public void ShowRenameDialog()
    {
        DialogManager.instance.ShowDialog(DialogIndex.RenameDialog);
    }
    public void ShowSettingDialog ()
    {
        DialogManager.instance.ShowDialog(DialogIndex.SettingDialog);
    }
    private void DataNameChange(object data)
    {
        name_lb.text = DataController.instance.GetName();
    }

    private void OnDisable()
    { 
        DataTrigger.UnRegisterValueChange(DataSchema.INVENTORY, DataGoldGemChange);
        DataTrigger.UnRegisterValueChange(DataSchema.INFO, DataNameChange);

    }
}
