public interface IPowerUpHolder
{
    IPowerUp HeldPowerUp { get; }
    void AddPowerUp(IPowerUp power);
}