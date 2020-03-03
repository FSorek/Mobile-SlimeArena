using System;
using Object = UnityEngine.Object;

public static class ScoreTracker
{
    public static event Action<int> OnScoreIncreased = delegate { };
    public static int Score { get; private set; }

    static ScoreTracker()
    {
        EnemyNPC.OnDeath += AddScore;
    }

    private static void AddScore(EnemyNPC npc)
    {
        Score++;
        OnScoreIncreased(Score);
    }
}