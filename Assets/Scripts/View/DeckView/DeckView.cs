using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DeckView : BaseView
{
    public DeckUIControl deckUIControl;
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        deckUIControl.Setup();
    }
    public void OnBack()
    {
        SoundManager.instance.OnPlayButtonSound();
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
}
