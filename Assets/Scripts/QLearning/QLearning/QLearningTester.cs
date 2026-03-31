using UnityEngine;

public class QLearningTester : MonoBehaviour
{
    private QLearningAgent agent;
    private RewardCalculator rewardCal;

    void Start()
    {
        agent = new QLearningAgent();
        rewardCal = new RewardCalculator();

        TestLoop();
    }

    void TestLoop()
    {
        PlayerState state = PlayerState.Balanced;

        for (int i = 0; i < 10; i++)
        {
            var action = agent.GetAction(state);

            PlayerPerformance p = new PlayerPerformance(0.6f, 40f, 2);

            float reward = rewardCal.Calculate(p);

            PlayerState nextState = PlayerState.Balanced;

            agent.UpdateQ(state, action, reward, nextState);
        }

        agent.DebugQTable();
    }
}