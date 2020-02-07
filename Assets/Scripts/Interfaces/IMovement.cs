public interface IMovement
{
    bool IsMoving { get; }
    void Initialize();
    void Move();
}