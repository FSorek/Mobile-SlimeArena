public interface ITakeDamage
{
    void TakeDamage(int damage);
    bool IsDead { get; }
}