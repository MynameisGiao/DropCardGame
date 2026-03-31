using UnityEngine;

public class PlayerPerformanceTracker : MonoBehaviour
{
    public float waveStartTime;
    public int deathCount = 0;

    public void StartWave()
    {
        waveStartTime = Time.time;
        deathCount = 0;
    }

    public void OnPlayerDeath()
    {
        deathCount++;
    }

public PlayerPerformance EndWave(float currentHP, float maxHP, int unitLoss)
{
    float clearTime = Time.time - waveStartTime;
    float hpPercent = currentHP / maxHP;

    PlayerPerformance performance = new PlayerPerformance(
        hpPercent,
        clearTime,
        unitLoss
    );

    Debug.Log($"[Performance] HP: {hpPercent}, Time: {clearTime}, Death: {unitLoss}");

    return performance;
}
}