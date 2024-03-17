using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConfigScene : BYSingletonMono<ConfigScene>
{
    [SerializeField]
    private List<Transform> enemy_spawns;
    [SerializeField]
    private List<Transform> targets; 
    
    public Transform GetEnemySpawnPoint()
    {
        // random theo tỉ lệ bằng nhau
       return enemy_spawns.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
    }
    public Transform GetRandomTarget()
    {
        // random theo tỉ lệ bằng nhau
        return targets.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
    }
}
