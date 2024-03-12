using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitData
{
    public ConfigEnemyRecord cf;
}
public class EnemyControl : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
       MissionManager.instance.EnemyDead(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void Setup(EnemyInitData enemyInitData)
    {

    }
}
