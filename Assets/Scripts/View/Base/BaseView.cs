using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : MonoBehaviour
{
    private BaseViewAnimation viewAnimation;
    public ViewIndex view_index;
    private void Awake()
    {
        viewAnimation=gameObject.GetComponentInChildren<BaseViewAnimation>();
    }
    public virtual void Setup(ViewParam param)
    {

    }
  
    private void HideView(Action callback)
    {
        viewAnimation.OnHideAnimation(() =>
        {
            gameObject.SetActive(false);
            OnHideView();
            callback?.Invoke();
           
        });
       
    }
    private void ShowView(object val)
    {
        viewAnimation.OnShowAnimation(() =>
        {
            Action callback = (Action)val;
            OnShowView();
            callback?.Invoke();
           
        });
       
    }
    public virtual void OnShowView()
    {

    }
    public virtual void OnHideView()
    {

    }

    
}
