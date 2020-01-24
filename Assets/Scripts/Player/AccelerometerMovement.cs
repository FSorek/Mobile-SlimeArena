using UnityEngine;

public class AccelerometerMovement : IMovement

{
    private readonly Player player;
    private readonly float moveSpeed;
    private readonly Rigidbody2D ownerRbody;
    private Vector2 direction;
    private bool isMoving;
    public bool IsMoving => isMoving;
    public AccelerometerMovement(Rigidbody2D rigidBody, float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        ownerRbody = rigidBody;
    }


    public void Initialize()
    {
        isMoving = true;
    }

    public void Move()
    {
        var acceleration = Input.acceleration;
        direction += new Vector2(acceleration.x, acceleration.y);

        direction = Vector2.ClampMagnitude(direction, 5f);
        ownerRbody.AddForce(moveSpeed * direction);
        
        if(ownerRbody.velocity.magnitude >= 10f){
            ownerRbody.velocity = Vector3.ClampMagnitude(ownerRbody.velocity, 10f);
        }
    }
}