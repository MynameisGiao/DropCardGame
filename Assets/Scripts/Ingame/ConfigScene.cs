using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConfigScene : BYSingletonMono<ConfigScene>
{
    public Transform range_mark_unit;
    public Material mark_unit_mat;
    [SerializeField]
    private List<Transform> enemy_spawns;
    [SerializeField]
    private List<Transform> targets;    
    
    private List<Transform> usedSpawnPoints = new List<Transform>();
    private List<Transform> usedTargetPoints = new List<Transform>();

    private void Start()
    {
        range_mark_unit.gameObject.SetActive(false);
    }
    public Transform GetEnemySpawnPoint()
    {
        if (enemy_spawns.Count == 0)
        {
            return null;
        }

        // Lọc những điểm spawn chưa được sử dụng
        List<Transform> availableSpawnPoints = enemy_spawns.Except(usedSpawnPoints).ToList();

        if (availableSpawnPoints.Count == 0)
        {
            usedSpawnPoints.Clear();
            availableSpawnPoints = new List<Transform>(enemy_spawns);
        }

        // Chọn một điểm spawn ngẫu nhiên từ những điểm còn lại
        int index = UnityEngine.Random.Range(0, availableSpawnPoints.Count);
        Transform selectedSpawnPoint = availableSpawnPoints[index];

        // Đánh dấu điểm spawn đã được sử dụng
        usedSpawnPoints.Add(selectedSpawnPoint);

        return selectedSpawnPoint;
    }

    public Transform GetRandomTarget()
    {
        //int index = UnityEngine.Random.Range(0, targets.Count);
        //return targets[index];
        if (targets.Count == 0)
        {
            return null;
        }

        // Lọc những điểm spawn chưa được sử dụng
        List<Transform> availableTargetPoints = targets.Except(usedTargetPoints).ToList();

        if (availableTargetPoints.Count == 0)
        {
            usedTargetPoints.Clear();
            availableTargetPoints = new List<Transform>(targets);
        }

        // Chọn một điểm spawn ngẫu nhiên từ những điểm còn lại
        int index = UnityEngine.Random.Range(0, availableTargetPoints.Count);
        Transform selectedTargetPoint = availableTargetPoints[index];

        // Đánh dấu điểm spawn đã được sử dụng
        usedTargetPoints.Add(selectedTargetPoint);

        return selectedTargetPoint;
    }
    public void SetMarkUnitRange(Vector3 pos,int range, bool isValid)
    {
        range_mark_unit.gameObject.SetActive(isValid);
        range_mark_unit.position = pos;
        range_mark_unit.localScale = Vector3.one * range *2;
    }
}
