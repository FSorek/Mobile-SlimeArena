using UnityEngine;

internal class InputMovement : IMovement
{
    private readonly PlayerInput input;
    private readonly Transform owner;

    public InputMovement(PlayerInput input, Transform owner)
    {
        this.input = input;
        this.owner = owner;
    }
    
    public void Initialize()
    {
        owner.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public bool Tick(float moveSpeed)
    {
        if (input.MoveVector.magnitude > 0)
        {
            owner.Translate(Time.deltaTime * moveSpeed * input.MoveVector.normalized);
            return true;
        }

        return false;
    }
}