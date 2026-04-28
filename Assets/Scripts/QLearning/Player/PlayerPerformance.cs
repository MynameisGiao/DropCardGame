using UnityEngine;

[System.Serializable]
public class PlayerPerformance
{
    public float hpRemaining;   // % máu còn lại (0 → 1)
    public float clearTime;     // thời gian clear wave
    public int deathCount;      // số lần chết trong wave

    public PlayerPerformance(float hp, float time, int death)
    {
        hpRemaining = hp;
        clearTime = time;
        deathCount = death;
    }
}