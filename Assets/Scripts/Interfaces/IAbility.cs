public interface IAbility
{
    float TickTime { get; }
    int Cost { get; }
    void StartedCasting();
    void Tick();
    void FinishedCasting();
}