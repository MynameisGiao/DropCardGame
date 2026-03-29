using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewIndex
{
    EmptyView =1,
    HomeView=2,
    IngameView=3,
    MissionView=4,
    DeckView=5,
    CollectionView=6,
    ShopView=7,
    RewardView=8
}
public class ViewParam
{

}

public class ViewConfig
{
    public static ViewIndex[] viewIndices =
    {
            ViewIndex.EmptyView,
            ViewIndex.HomeView,
            ViewIndex.IngameView,
            ViewIndex.MissionView,
            ViewIndex.DeckView,
            ViewIndex.CollectionView,
            ViewIndex.ShopView,
            ViewIndex.RewardView,
    };

}