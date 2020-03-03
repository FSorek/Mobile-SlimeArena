public interface IAbilityPool
{
    float CurrentPoolPercentage { get; }
    int CurrentPoolAmount { get; }
    void Reduce(int abilityCost);
    void Restore(int amount);
}