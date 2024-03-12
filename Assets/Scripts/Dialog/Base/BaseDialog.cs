using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseDialog : MonoBehaviour
{
    public DialogIndex dialogIndex;
    public BaseDialogAnimation dialogAnimation;
  
    private void Awake()
    {
        dialogAnimation = gameObject.GetComponentInChildren<BaseDialogAnimation>();
    }

   
    public virtual void Setup(DialogParam param)
    {

    }
    private void HideDialog(Action callback)
    {
        dialogAnimation.OnHideAnimation(() =>
        {
            gameObject.SetActive(false);

            OnHideDialog();
            callback?.Invoke();
        });

    }
    public virtual void OnShowDialog()
    {

    }
    private void ShowDialog(object val)
    {
        dialogAnimation.OnShowAnimation(() =>
        {
            Action callback = (Action)val;
            OnShowDialog();

            callback?.Invoke();
        });

    }
    public virtual void OnHideDialog()
    {

    }
}
