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
    ShopView=6,
    RewardView=7
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
            ViewIndex.ShopView,
            ViewIndex.RewardView,
    };

}