using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.VersionControl;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class MissionManager : BYSingletonMono<MissionManager>
{
    public ConfigMissionRecord cf_mission;
    private List<int> waves;
    private int index_wave = -1;
    private int number_enemy_dead;
    private int total_enemy;
    private int count_enemy_create;
    public UnityEvent <int, int> OnWaveChange;
    private int hp = 50;
    private int max_hp = 50;
    public  UnityEvent<int, int> OnBaseHpChange;


    private bool isEndMission = false;
   
    // Start is called before the first frame update
    IEnumerator Start()
    {
      
        cf_mission = GameManager.instance.cur_cf_mission;
        waves = cf_mission.Waves;
        yield return new WaitForSeconds(10);
        StopCoroutine("CreateNewWave");
        StartCoroutine("CreateNewWave");
    }
    IEnumerator CreateNewWave()
    {
        index_wave++;
        if (index_wave >= waves.Count)
        {
            // mission complete
            OnWaveChange.RemoveAllListeners();
            OnBaseHpChange.RemoveAllListeners();
            Debug.LogError("Mission complete");
            BYPoolManager.instance.GetPool("HPHub").DeSpawnAll();
            WinDialogParam param = new WinDialogParam();
            param.cf_mission = cf_mission;
            DialogManager.instance.ShowDialog(DialogIndex.WinDialog, param);
           
        }
        else
        {
            ConfigWaveRecord cf_wave = ConfigManager.instance.configWave.GetRecordByKeySearch(waves[index_wave]);
            total_enemy = cf_wave.Enemies.Count;
            count_enemy_create = 0;
            number_enemy_dead = 0;
            OnWaveChange?.Invoke(index_wave + 1, waves.Count);
            yield return new WaitForSeconds(cf_wave.Time_Delay);
           

            for (int i = 0; i < cf_wave.Enemies.Count; i++)
            {
                // tạo enemy
                StartCoroutine(CreateEnemy(cf_wave.Time_Spawns[i], cf_wave.Enemies[i]));
            }
        }
    }
    IEnumerator CreateEnemy(int delay, int id)
    {
        yield return new WaitForSeconds(delay);
        // create enemy
        count_enemy_create++;
        ConfigEnemyRecord cf_enemy = ConfigManager.instance.configEnemy.GetRecordByKeySearch(id);
        GameObject e_obj = Instantiate(Resources.Load("Enemy/" + cf_enemy.Prefab, typeof(GameObject))) as GameObject;
        Transform pos_trans = ConfigScene.instance.GetEnemySpawnPoint();
        e_obj.transform.position = pos_trans.position;
        e_obj.transform.forward = pos_trans.forward;
        EnemyControl enemyControl = e_obj.GetComponent<EnemyControl>();
        enemyControl.Setup(new EnemyInitData { cf = cf_enemy });
    }
    public void EnemyDead(EnemyControl e)
    {
        number_enemy_dead++;
        if (count_enemy_create >= total_enemy)
        {
            if (number_enemy_dead >= total_enemy)
            {
                StartCoroutine("CreateNewWave");
            }
        }
    }

    public void StartMission()
    {
        index_wave = -1;
        number_enemy_dead = 0;
        total_enemy = 0;
        count_enemy_create = 0;
        StopAllCoroutines();
        StartCoroutine("Start");
    }

    // base chịu damageData từ enemy
    public void OnDamage(int damage_e)
    {
        hp -= damage_e;
        OnBaseHpChange?.Invoke(hp, max_hp);
        if (hp<=0 )
        {
            hp = 0;
            if(!isEndMission)
            {
                BYPoolManager.instance.GetPool("HPHub").DeSpawnAll();
                DialogManager.instance.ShowDialog(DialogIndex.FailDialog);
                isEndMission = true;
            }
           
        }
        
    }

    public void OnCreateUnit(UnitData unitData, ConfigUnitRecord cf_unit, Vector3 posCreate)
    {

        GameObject unit = Instantiate(Resources.Load("Unit/" + cf_unit.Prefab, typeof(GameObject))) as GameObject;
        unit.transform.position = posCreate;
        unit.GetComponent<UnitControl>().Setup(new UnitInitData { configUnit = cf_unit, unitData = unitData });

    }
}
