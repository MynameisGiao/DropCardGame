using UnityEngine;

public class StateEvaluator
{
    private float lowHPThreshold = 0.4f;
    private float highHPThreshold = 0.8f;

    private int highDeathThreshold = 5;
    private int lowDeathThreshold = 1;

    private float fastTimeThreshold = 40f;

    public PlayerState GetState(PlayerPerformance p)
    {
        //  Struggling
        if (p.hpRemaining < lowHPThreshold || p.deathCount >= highDeathThreshold)
        {
            return PlayerState.Struggling;
        }

        //  Dominating
        if (p.hpRemaining > highHPThreshold &&
            p.deathCount <= lowDeathThreshold &&
            p.clearTime < fastTimeThreshold)
            {
                return PlayerState.Dominating;
            }

        //  Balanced
        return PlayerState.Balanced;
    }
}