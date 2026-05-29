using System.Collections.Generic;
using UnityEngine;

public class QLearningAgent
{
    private Dictionary<PlayerState, Dictionary<DifficultyAction, float>> qTable;

    // Learning rate: hoc tu tu de Q-value on dinh, khong bi nhay manh sau 1 wave.
    public float alpha = 0.1f;
    // Discount factor: uu tien trai nghiem dai han, khong chi reward cua wave hien tai.
    public float gamma = 0.9f;
    // Exploration rate: 50% thu action moi de agent kham pha chien luoc tot hon khi dang hoc.
    public float epsilon = 0.5f;

    public QLearningAgent()
    {
        qTable = new Dictionary<PlayerState, Dictionary<DifficultyAction, float>>();

        foreach (PlayerState state in System.Enum.GetValues(typeof(PlayerState)))
        {
            qTable[state] = new Dictionary<DifficultyAction, float>();

            foreach (DifficultyAction action in System.Enum.GetValues(typeof(DifficultyAction)))
            {
                qTable[state][action] = 0f;
            }
        }
    }

    //  CHỌN ACTION
    public DifficultyAction GetAction(PlayerState state)
    {
        // explore
        if (Random.value < epsilon)
        {
            return (DifficultyAction)Random.Range(0, System.Enum.GetValues(typeof(DifficultyAction)).Length);
        }

        // exploit
        var actions = qTable[state];

        DifficultyAction bestAction = DifficultyAction.Keep;
        float maxValue = float.MinValue;

        foreach (var pair in actions)
        {
            if (pair.Value > maxValue)
            {
                maxValue = pair.Value;
                bestAction = pair.Key;
            }
        }

        return bestAction;
    }

    //  UPDATE Q
    public void UpdateQ(PlayerState state, DifficultyAction action, float reward, PlayerState nextState)
    {
        float currentQ = qTable[state][action];

        float maxNextQ = float.MinValue;
        foreach (var q in qTable[nextState].Values)
        {
            if (q > maxNextQ)
                maxNextQ = q;
        }

        float newQ = currentQ + alpha * (reward + gamma * maxNextQ - currentQ);

        qTable[state][action] = newQ;

        // Q(s,a) = Q(s,a) + alpha * (reward + gamma * maxQ(s') - Q(s,a))
        // state = s
        // action = a
        // currentQ = Q(s,a) hiện tại
        // reward = phần thưởng vừa nhận
        // nextState = s'
        // maxNextQ = maxQ(s')
        // alpha = learning rate
        // gamma = discount factor

        Debug.Log($"[QTABLE] Q[{state}][{action}] = {newQ:F2}");
    }

    //  DEBUG
    public void DebugQTable()
    {
        foreach (var state in qTable)
        {
            string row = state.Key + ": ";

            foreach (var action in state.Value)
            {
                row += $"{action.Key}={action.Value:F2} ";
            }

            Debug.Log(row);
        }
    }

    //  SAVE
    public void Save()
    {
        QTableData data = new QTableData
        {
            struggling = ConvertToArray(qTable[PlayerState.Struggling]),
            balanced = ConvertToArray(qTable[PlayerState.Balanced]),
            dominating = ConvertToArray(qTable[PlayerState.Dominating])
        };

        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString("QTABLE", json);
        PlayerPrefs.Save();

        Debug.Log("[AI] Q-table saved");
    }

    //  LOAD
    public void Load()
    {
        if (!PlayerPrefs.HasKey("QTABLE"))
        {
            Save();
            Debug.Log("[AI] No saved Q-table, created default Q-table");
            return;
        }

        string json = PlayerPrefs.GetString("QTABLE");

        QTableData data = JsonUtility.FromJson<QTableData>(json);

        ConvertToDict(PlayerState.Struggling, data.struggling);
        ConvertToDict(PlayerState.Balanced, data.balanced);
        ConvertToDict(PlayerState.Dominating, data.dominating);

        Debug.Log("[AI] Q-table loaded");
    }

    //  SYNC TO SO
    public void LoadToSO(QTableSO so)
    {
        so.struggling = ConvertToList(qTable[PlayerState.Struggling]);
        so.balanced = ConvertToList(qTable[PlayerState.Balanced]);
        so.dominating = ConvertToList(qTable[PlayerState.Dominating]);
    }
    public void LoadFromSO(QTableSO so)
    {
        ConvertFromList(PlayerState.Struggling, so.struggling);
        ConvertFromList(PlayerState.Balanced, so.balanced);
        ConvertFromList(PlayerState.Dominating, so.dominating);
    }

    private void ConvertFromList(PlayerState state, List<QActionEntry> list)
    {
        foreach (var entry in list)
        {
            qTable[state][entry.action] = entry.value;
        }
    }
    // HELPER
    private float[] ConvertToArray(Dictionary<DifficultyAction, float> dict)
    {
        float[] arr = new float[System.Enum.GetValues(typeof(DifficultyAction)).Length];

        foreach (var pair in dict)
        {
            arr[(int)pair.Key] = pair.Value;
        }

        return arr;
    }

    private void ConvertToDict(PlayerState state, float[] arr)
    {
        foreach (DifficultyAction action in System.Enum.GetValues(typeof(DifficultyAction)))
        {
            int index = (int)action;
            if (index < arr.Length)
            {
                qTable[state][action] = arr[index];
            }
        }
    }
    private List<QActionEntry> ConvertToList(Dictionary<DifficultyAction, float> dict)
    {
        List<QActionEntry> list = new List<QActionEntry>();

        foreach (var pair in dict)
        {
            list.Add(new QActionEntry
            {
                action = pair.Key,
                value = pair.Value
            });
        }

        return list;
    }

}
