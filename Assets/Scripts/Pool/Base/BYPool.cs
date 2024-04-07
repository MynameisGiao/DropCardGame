using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BYPool 
{
    public int total;
    public string name_pool;
    public Transform prefab;
    [NonSerialized]
    public List<Transform> elements = new List<Transform>();
    private int index = -1;
    public BYPool()
    {

    }
    public BYPool(string name_,int total_,Transform prefab_)
    {
        this.name_pool = name_;
        this.total = total_;
        this.prefab = prefab_;
    }

    public Transform Spawn()
    {
        index++;
        if(index>=elements.Count)
        {
            index = 0;
        }
        Transform trans = elements[index];
        trans.gameObject.SetActive(true);
        trans.gameObject.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);
        return trans;

    }
    public void Despawn(Transform trans_)
    {
        
        if (elements.Contains(trans_))
        {
            trans_.SetParent(BYPoolManager.instance.transform);
            trans_.gameObject.SendMessage("OnDespawn", SendMessageOptions.DontRequireReceiver);
            trans_.gameObject.SetActive(false);
        }
      

    }
    public void DeSpawnAll()
    {
        foreach(Transform trans_ in elements)
        {
            trans_.SetParent(BYPoolManager.instance.transform);
            trans_.gameObject.SendMessage("OnDespawn", SendMessageOptions.DontRequireReceiver);
            trans_.gameObject.SetActive(false);
        }
    }
    
}
