using UnityEngine;

public class AccelerometerMovement : IMovement

{
    private readonly Transform owner;
    private readonly Rigidbody2D ownerRbody;
    private Vector2 direction;
    public AccelerometerMovement(Transform owner)
    {
        this.owner = owner;
        ownerRbody = owner.GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        owner.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public bool Tick(float moveSpeed)
    {
        var acceleration = Input.acceleration;
        direction += new Vector2(acceleration.x, acceleration.y);

        direction = Vector2.ClampMagnitude(direction, 5f);
        ownerRbody.AddForce(moveSpeed * direction);
        
        if(ownerRbody.velocity.magnitude >= 10f){
            ownerRbody.velocity = Vector3.ClampMagnitude(ownerRbody.velocity, 10f);
        }
        
        return true;
    }
}