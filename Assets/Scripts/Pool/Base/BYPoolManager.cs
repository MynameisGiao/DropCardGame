using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BYPoolManager : BYSingletonMono<BYPoolManager>
{
    [SerializeField]
    List<BYPool> pools;
    private Dictionary<string, BYPool> dic_pool = new Dictionary<string, BYPool>();
    // Start is called before the first frame update
    void Awake()
    {
        foreach(BYPool p in pools)
        {
            CreatePrefab(p);
            dic_pool.Add(p.name_pool, p);
        }
    }
    public void AddPool(BYPool p)
    {
        if(!dic_pool.ContainsKey(p.name_pool))
        {
            CreatePrefab(p);
            dic_pool.Add(p.name_pool, p);
        }
    }
    private void CreatePrefab(BYPool p)
    {
        for(int i=0;i<p.total;i++)
        {
            Transform trans = Instantiate(p.prefab, Vector3.zero, Quaternion.identity);
            p.elements.Add(trans);
            trans.gameObject.SetActive(false);
            trans.SetParent(transform);
        }
    }
    public BYPool GetPool(string name_)
    {
        return dic_pool[name_];
    }
    // Update is called once per frame
    private void OnDestroy()
    {
        dic_pool.Clear();
    }
}
