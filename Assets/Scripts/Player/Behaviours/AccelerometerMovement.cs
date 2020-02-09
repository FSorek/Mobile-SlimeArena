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
    private Matrix4x4 calibrationMatrix;
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

        var rotation = Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), Input.acceleration);
        calibrationMatrix = Matrix4x4.TRS(Vector3.zero, rotation, new Vector3(1, 1, 1)).inverse;
    }

    public void Move(float speedPercentage = 1f)
    {
        var acceleration = calibrationMatrix.MultiplyVector(Input.acceleration);
        direction = new Vector2(acceleration.x, acceleration.y).normalized;
        ownerRbody.velocity += accelerationSpeed * Time.fixedDeltaTime * direction;

        if(ownerRbody.velocity.magnitude >= maxSpeed){
            ownerRbody.velocity = Vector3.ClampMagnitude(ownerRbody.velocity, maxSpeed);
        }
    }
}