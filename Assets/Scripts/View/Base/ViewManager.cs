using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : BYSingletonMono<ViewManager>
{
    public event Action<BaseView> OnViewHide;
    public event Action<BaseView> OnViewShow;
    public RectTransform anchor_view;
    private Dictionary<ViewIndex, BaseView> dic_View = new Dictionary<ViewIndex, BaseView>();
    public BaseView cur_view = null;
    void Start()
    {
        foreach (ViewIndex viewIndex in ViewConfig.viewIndices)
        {
            string name_v = viewIndex.ToString();
            GameObject go_view = Instantiate(Resources.Load("View/" + name_v, typeof(GameObject))) as GameObject;
            go_view.transform.SetParent(anchor_view, false);
            go_view.SetActive(false);
            BaseView view = go_view.GetComponent<BaseView>();
            dic_View.Add(view.view_index, view);
        }
        SwitchView(ViewIndex.EmptyView);
    }

    public void SwitchView(ViewIndex viewIndex, ViewParam param = null, Action callback = null)
    {
        if (cur_view != null)
        {
            OnViewHide?.Invoke(cur_view);
            Action cb = () =>
            {
               
                // hide view 
                cur_view = dic_View[viewIndex];
                ShowNewView(param, callback);
            };
            cur_view.SendMessage("HideView", (object)cb, SendMessageOptions.RequireReceiver);

        }
        else
        {
            cur_view = dic_View[viewIndex];
            ShowNewView(param, callback);
        }
    }
    private void ShowNewView(ViewParam param = null, Action callback = null)
    {
        cur_view.gameObject.SetActive(true);
        cur_view.Setup(param);
        OnViewShow?.Invoke(cur_view);

        Action cb = () =>
        {
            callback?.Invoke();
        };
        cur_view.SendMessage("ShowView", (object)cb, SendMessageOptions.RequireReceiver);

    }


}
