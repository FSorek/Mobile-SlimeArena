public class AbilityPool : IAbilityPool
{
    private readonly int maxPool;
    public float CurrentPoolPercentage => (float)CurrentPoolAmount / maxPool;
    public int CurrentPoolAmount { get; private set; }

    public AbilityPool(int maxPool)
    {
        this.maxPool = maxPool;
        CurrentPoolAmount = maxPool;
    }

    public void Reduce(int abilityCost)
    {
        CurrentPoolAmount -= abilityCost;
        if (CurrentPoolAmount < 0)
            CurrentPoolAmount = 0;
    }

    public void Restore(int amount)
    {
        CurrentPoolAmount += amount;
        if (CurrentPoolAmount > maxPool)
            CurrentPoolAmount = maxPool;
    }
}