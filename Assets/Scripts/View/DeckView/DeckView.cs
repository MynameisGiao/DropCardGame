using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DeckView : BaseView
{
    public DeckUIControl deckUIControl;
    public DeckCollectionControl deckCollectionControl;
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        deckUIControl.Setup();
        deckCollectionControl.Setup();

    }
    public void OnBack()
    {
        SoundManager.instance.OnPlayButtonSound();
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
}
