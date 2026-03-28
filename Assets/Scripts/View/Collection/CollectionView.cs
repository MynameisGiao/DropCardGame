using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectionView : BaseView
{
    public CollectionControl collectionControl;
    private ButtonTypeCollection[] _typeButtons;

    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        
        // Find all button type collection in this view
        _typeButtons = GetComponentsInChildren<ButtonTypeCollection>();
        foreach (var btn in _typeButtons)
        {
            btn.SetCollectionView(this);
        }
        
        collectionControl.SetupUnits(TypeCollection.All);
        
        // Set initial button selection state
        foreach (var btn in _typeButtons)
        {
            btn.UpdateButtonSelection(TypeCollection.All);
        }
    }
    public void OnClickTypeCollection(TypeCollection type)
    {
        switch (type)
        {
            case TypeCollection.All:
                collectionControl.SetupUnits(TypeCollection.All);
                break;
            case TypeCollection.Legendary:
                collectionControl.SetupUnits(TypeCollection.Legendary);
                break;
            case TypeCollection.Epic:
                collectionControl.SetupUnits(TypeCollection.Epic);
                break;
            case TypeCollection.Common:
                collectionControl.SetupUnits(TypeCollection.Common);
                break;
        }
        
        // Update button selection state
        if (_typeButtons != null)
        {
            foreach (var btn in _typeButtons)
            {
                btn.UpdateButtonSelection(type);
            }
        }
    }

    
    public void OnBack()
    {
        SoundManager.instance.OnPlayButtonSound();
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
}

