using UnityEngine;

internal class InputMovement : IMovement
{
    private readonly PlayerInput input;
    private readonly Rigidbody2D owner;

    public InputMovement(PlayerInput input, Transform owner)
    {
        this.input = input;
        this.owner = owner.GetComponent<Rigidbody2D>();
    }
    
    public void Initialize()
    {
        owner.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public bool Tick(float moveSpeed)
    {
        if (input.MoveVector.magnitude > 0)
        {
            owner.MovePosition(owner.position + Time.fixedDeltaTime * moveSpeed * input.MoveVector.normalized);
            return true;
        }

        return false;
    }
}