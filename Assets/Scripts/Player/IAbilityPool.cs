public interface IAbilityPool
{
    float CurrentPoolPercentage { get; }
    int CurrentPoolAmount { get; }
}

public class PlayerPool : IAbilityPool
{
    private readonly int maxPool;
    public float CurrentPoolPercentage => CurrentPoolAmount / maxPool;
    public int CurrentPoolAmount { get; private set; }

    public PlayerPool(int maxPool)
    {
        this.maxPool = maxPool;
        CurrentPoolAmount = maxPool;
    }
}