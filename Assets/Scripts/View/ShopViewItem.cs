using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopViewItem : MonoBehaviour
{
    public Image icon;
    public TMP_Text name_lb;
    public TMP_Text value_lb;
    public TMP_Text price_lb;

    public GameObject topglow_red;
    public GameObject topglow_blue;
    public GameObject bg_red;
    public GameObject bg_blue;
    public GameObject gold_icon;
    public GameObject gem_icon;

    private ConfigShopRecord cf;
    
   public void Setup(ConfigShopRecord cf)
    {
        this.cf = cf;
        name_lb.text = cf.Name;
        value_lb.text = cf.Value.ToString();
        price_lb.text = cf.Price.ToString();
        icon.overrideSprite = SpriteLibControl.instance.GetSpriteByName(cf.Image);
        if(cf.Shop_type==1)
        {
            topglow_red.SetActive(true);
            topglow_blue.SetActive(false);

            bg_red.SetActive(true);
            bg_blue.SetActive(false);

            gold_icon.SetActive(false);
            gem_icon.SetActive(true) ;
        }
        else 
        {
            topglow_blue.SetActive(true);
            topglow_red.SetActive(false);

            bg_blue.SetActive(true);
            bg_red.SetActive(false);

            gem_icon.SetActive(false);
            gold_icon.SetActive(true);
        }
    }
    public void OnBuy()
    {
        DataController.instance.OnShopBuy(cf);
    }
}
