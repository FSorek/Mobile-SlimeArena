public interface IMovement
{
    bool IsMoving { get; }
    void Initialize();
    void Move(float speedPercentage = 1f);
}