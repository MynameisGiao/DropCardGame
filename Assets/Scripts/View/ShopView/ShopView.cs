using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : BaseView
{
    public Transform parent_item;
    public ShopViewItem prefab_item;
    private List<ShopViewItem> items = new List<ShopViewItem>();
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        List<ConfigShopRecord> configShops = ConfigManager.instance.configShop.records;
        if(items.Count ==0)
        {
            for(int i=0;i<configShops.Count; i++)
            {
                ShopViewItem item = Instantiate(prefab_item);
                items.Add(item);
                item.transform.SetParent(parent_item, false); // false: reset transfrom về 0, để layoutgourp quản lý                                                             
            }
        }

        for(int i=0; i<configShops.Count; i++)
        {
            items[i].Setup(configShops[i]);
        }
    }
    public void OnBack()
    {
        SoundManager.instance.OnPlayButtonSound();
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
}
