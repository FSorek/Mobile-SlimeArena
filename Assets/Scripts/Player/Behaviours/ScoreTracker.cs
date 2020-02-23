using System;
using Object = UnityEngine.Object;

public static class ScoreTracker
{
    public static event Action<int> OnScoreIncreased = delegate { };
    public static int Score { get; private set; }

    static ScoreTracker()
    {
        var player = Object.FindObjectOfType<Player>();
        player.PlayerAttack.OnTargetHit += OnTargetHit;
    }

    private static void OnTargetHit(ITakeDamage target)
    {
        if (!target.Health.IsDead)
            return;
        Score++;
        OnScoreIncreased(Score);
    }
}