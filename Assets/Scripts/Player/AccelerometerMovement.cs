using UnityEngine;

public class AccelerometerMovement : IMovement

{
    private readonly Player player;
    private readonly float accelerationSpeed;
    private readonly float maxSpeed;
    private readonly Rigidbody2D ownerRbody;
    private Vector2 direction;
    private bool isMoving;
    private Vector3 initialAcceleration;
    public bool IsMoving => isMoving;
    public AccelerometerMovement(Rigidbody2D rigidBody, float accelerationSpeed, float maxSpeed)
    {
        this.accelerationSpeed = accelerationSpeed;
        this.maxSpeed = maxSpeed;
        ownerRbody = rigidBody;
    }
    
    public void Initialize()
    {
        isMoving = true;
        ownerRbody.velocity = Vector2.zero;
        ownerRbody.inertia = 0f;
        ownerRbody.angularVelocity = 0f;
        initialAcceleration = Input.acceleration;
    }

    public void Move()
    {
        var acceleration = Input.acceleration - initialAcceleration;
        Debug.Log(acceleration);
        direction += new Vector2(acceleration.x, acceleration.y);
        ownerRbody.AddForce(accelerationSpeed * direction.normalized);
        
        if(ownerRbody.velocity.magnitude >= 10f){
            ownerRbody.velocity = Vector3.ClampMagnitude(ownerRbody.velocity, maxSpeed);
        }
    }
}