using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopViewAnimation : BaseViewAnimation
{
    public Animator animator;
    private Action callback;
    public override void OnHideAnimation(Action callback)
    {
        this.callback = callback;
        animator.Play("Hide", 0, 0);

    }
    public override void OnShowAnimation(Action callback)
    {
        this.callback = callback;
        animator.Play("Show", 0, 0);

    }
    public void ShowEnd()
    {
        callback();
    }
    public void HideEnd()
    {
        callback();
    }
}
