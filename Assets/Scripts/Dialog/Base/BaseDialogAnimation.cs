using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class BaseDialogAnimation : MonoBehaviour
{
    private CanvasGroup canvas_group;
    private void Awake()
    {
        canvas_group = gameObject.GetComponent<CanvasGroup>();
        canvas_group.alpha = 0;
    }
   
    public virtual void OnHideAnimation(Action callback)
    {
        float alpha = 1;
        DOTween.To(() => alpha, x => alpha = x, 0, 0.5f).OnUpdate(() =>
        {
            canvas_group.alpha = alpha;

        }).OnComplete(() =>
        {
            callback?.Invoke();
        }).SetUpdate(true);
    }
    public virtual void OnShowAnimation(Action callback)
    {
        float alpha = 0;
        DOTween.To(() => alpha, x => alpha = x, 1, 0.5f).OnUpdate(() =>
        {
            canvas_group.alpha = alpha;

        }).OnComplete(() =>
        {
            callback?.Invoke();
        }).SetUpdate(true);
    }
    private void Reset()
    {
        gameObject.name = this.GetType().ToString();
    }
}
