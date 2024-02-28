using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BaseViewAnimation : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }
    public virtual void OnHideAnimation(Action callback)
    {
        float alpha = 1;
        DOTween.To(() => alpha, x => alpha = x, 0, 0.5f).OnUpdate(() =>
        {
            canvasGroup.alpha = alpha;
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
            canvasGroup.alpha = alpha;
        }).OnComplete(() =>
        {
            callback?.Invoke();
        }).SetUpdate(true);
    }
    private void Reset()
    {
        gameObject.name=this.GetType().ToString();
    }

}
