using System;

public class PlayerScoreTracker
{
    public event Action<int> OnScoreIncreased = delegate { };
    private int score;

    public void AddScore()
    {
        score++;
        OnScoreIncreased(score);
    }
    public int Score => score;
}