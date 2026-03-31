using UnityEngine;

public class RewardCalculator
{
    public float Calculate(PlayerPerformance p)
    {
        if (p.hpRemaining <= 0)
            return -10f;

        // quá khó
        if (p.deathCount >= 7)
            return -5f;

        // quá dễ
        if (p.deathCount == 0)
            return -2f;

        // lý tưởng
        if (p.deathCount >= 2 && p.deathCount <= 4)
            return +5f;

        return +2f;
    }
}