using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ConfigShopRecord
{
   // id name    image price   value shop_type

    [SerializeField]
    private int id;
    public int ID
    {
        get
        {
            return id;
        }
    }
    [SerializeField]
    private string name;
    public string Name
    {
        get
        {
            return name;
        }
    }
    [SerializeField]
    private string image;
    public string Image
    {
        get
        {
            return image;
        }
    }
    [SerializeField]
    private int price;
    public int Price
    {
        get
        {
            return price;
        }
    }
    [SerializeField]
    private int value;
    public int Value
    {
        get
        {
            return value;
        }
    }
    [SerializeField]
    private int shop_type;
    public int Shop_type
    {
        get
        {
            return shop_type;
        }
    }
}
public class ConfigShop : BYDataTable<ConfigShopRecord>
{
    public override ConfigCompare<ConfigShopRecord> DefindCompare()
    {
        configCompare = new ConfigCompare<ConfigShopRecord>("id");
        return configCompare;
    }
}
