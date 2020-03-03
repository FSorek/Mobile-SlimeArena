public interface IAbility
{
    float TickTime { get; }
    float Cost { get; }
    void StartedCasting();
    void Tick();
    void FinishedCasting();
}