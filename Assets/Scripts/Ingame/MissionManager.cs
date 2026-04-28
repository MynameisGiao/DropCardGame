using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.VersionControl;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum GameMode
{
    Static,
    QLearning
}
public class MissionManager : BYSingletonMono<MissionManager>
{
    [SerializeField] private QTableSO qTableSO;
    [Header("Mode")]
    public GameMode gameMode = GameMode.QLearning;

    [Header("Performance")]
    [SerializeField] private PlayerPerformanceTracker _playerPerformTracker;
    private int unitDeadCount = 0;

    [Header("Mission")]
    public ConfigMissionRecord cf_mission;
    private List<int> waves;
    private int index_wave = -1;

    private int number_enemy_dead;
    private int total_enemy;
    private int count_enemy_create;

    public UnityEvent<int, int> OnWaveChange;

    private int hp = 50;
    private int max_hp = 50;
    public UnityEvent<int, int> OnBaseHpChange;

    private bool isEndMission = false;

    // AI
    private QLearningAgent agent;
    private RewardCalculator rewardCalculator;
    private StateEvaluator stateEvaluator;

    private PlayerState lastState;
    private DifficultyAction lastAction;

    // modifier
    private int bonusEnemy = 0;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        agent = new QLearningAgent();
        agent.Load();
        agent.LoadToSO(qTableSO);
        agent.DebugQTable();
        rewardCalculator = new RewardCalculator();
        stateEvaluator = new StateEvaluator();

        cf_mission = GameManager.instance.cur_cf_mission;
        waves = cf_mission.Waves;

        yield return new WaitForSeconds(8);
        StartCoroutine(CreateNewWave());
    }
    IEnumerator CreateNewWave()
    {
        index_wave++;

        if (index_wave >= waves.Count)
            if (index_wave >= waves.Count)
            {
                agent.Save();

                OnWaveChange.RemoveAllListeners();
                OnBaseHpChange.RemoveAllListeners();
                Debug.LogError("Mission complete");

                BYPoolManager.instance.GetPool("HPHub").DeSpawnAll();

                WinDialogParam param = new WinDialogParam();
                param.cf_mission = cf_mission;
                DialogManager.instance.ShowDialog(DialogIndex.WinDialog, param);

                yield break;
            }
        unitDeadCount = 0;

        ConfigWaveRecord cf_wave = ConfigManager.instance.configWave.GetRecordByKeySearch(waves[index_wave]);

        // reset
        bonusEnemy = 0;

        if (gameMode == GameMode.QLearning)
        {
            switch (lastAction)
            {
                case DifficultyAction.IncreaseSpawn:
                    bonusEnemy = 2;
                    break;

                case DifficultyAction.DecreaseSpawn:
                    bonusEnemy = -1;
                    break;

                case DifficultyAction.TightenSpawn:
                    // xử lý ở time spawn
                    break;
            }
        }

        total_enemy = Mathf.Max(1, cf_wave.Enemies.Count + bonusEnemy);
        count_enemy_create = 0;
        number_enemy_dead = 0;

        OnWaveChange?.Invoke(index_wave + 1, waves.Count);

        yield return new WaitForSeconds(cf_wave.Time_Delay);

        _playerPerformTracker.StartWave();

        int totalSpawn = Mathf.Max(1, cf_wave.Enemies.Count + bonusEnemy);

        for (int i = 0; i < totalSpawn; i++)
        {
            int index = i % cf_wave.Enemies.Count;

            float baseTime = cf_wave.Time_Spawns[index];
            float spawnTime = baseTime;

            if (gameMode == GameMode.QLearning)
            {
                switch (lastAction)
                {
                    case DifficultyAction.IncreaseSpawn:
                        spawnTime = baseTime + (i * 0.2f);
                        break;

                    case DifficultyAction.DecreaseSpawn:
                        spawnTime = baseTime;
                        break;

                    case DifficultyAction.TightenSpawn:
                        spawnTime = baseTime * 0.7f;
                        break;
                }
            }


            StartCoroutine(CreateEnemy(
                Mathf.RoundToInt(spawnTime),
                cf_wave.Enemies[index]
            ));
        }
        Debug.Log($"[SPAWN] Base: {cf_wave.Enemies.Count}, Final: {totalSpawn}, Action: {lastAction}");
    }

    IEnumerator CreateEnemy(int delay, int id)
    {
        yield return new WaitForSeconds(delay);

        count_enemy_create++;

        ConfigEnemyRecord cf_enemy = ConfigManager.instance.configEnemy.GetRecordByKeySearch(id);

        if (cf_enemy == null)
        {
            Debug.LogError($"Enemy config NULL với id: {id}");
            yield break;
        }

        GameObject e_obj = Instantiate(Resources.Load("Enemy/" + cf_enemy.Prefab)) as GameObject;

        Transform pos = ConfigScene.instance.GetEnemySpawnPoint();
        e_obj.transform.position = pos.position;
        e_obj.transform.forward = pos.forward;

        EnemyControl enemy = e_obj.GetComponent<EnemyControl>();
        enemy.Setup(new EnemyInitData { cf = cf_enemy });
    }

    public void EnemyDead(EnemyControl e)
    {
        number_enemy_dead++;

        if (count_enemy_create >= total_enemy && number_enemy_dead >= total_enemy)
        {
            if (!isEndMission)
            {
                var performance = _playerPerformTracker.EndWave(hp, max_hp, unitDeadCount);

                Debug.Log($"[Wave Done] HP: {performance.hpRemaining}, Time: {performance.clearTime}, Death: {performance.deathCount}");

                // CHỈ CHẠY AI KHI QLearning
                if (gameMode == GameMode.QLearning)
                {
                    PlayerState currentState = stateEvaluator.GetState(performance);
                    float reward = rewardCalculator.Calculate(performance);

                    if (index_wave > 0)
                    {
                        agent.UpdateQ(lastState, lastAction, reward, currentState);
                    }

                    DifficultyAction action = agent.GetAction(currentState);

                    lastState = currentState;
                    lastAction = action;

                    Debug.Log($"[AI] State: {currentState} | Action: {action} | Reward: {reward}");
                    agent.LoadToSO(qTableSO);
                }
            }

            StartCoroutine(CreateNewWave());
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
    public void OnDamage(int damage)
    {
        hp -= damage;
        OnBaseHpChange?.Invoke(hp, max_hp);

        if (hp <= 0)
        {
            hp = 0;
            agent.Save();
            isEndMission = true;
        }
    }

    public void OnCreateUnit(UnitData unitData, ConfigUnitRecord cf_unit, Vector3 posCreate)
    {

        GameObject unit = Instantiate(Resources.Load("Unit/" + cf_unit.Prefab, typeof(GameObject))) as GameObject;
        unit.transform.position = posCreate;
        unit.GetComponent<UnitControl>().Setup(new UnitInitData { configUnit = cf_unit, unitData = unitData });

    }
    public void OnUnitDead()
    {
        unitDeadCount++;
    }
}
