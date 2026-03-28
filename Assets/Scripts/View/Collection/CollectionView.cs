using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectionView : BaseView
{
    public CollectionControl collectionControl;
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        collectionControl.Setup();

    }
    public void OnBack()
    {
        SoundManager.instance.OnPlayButtonSound();
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
}

