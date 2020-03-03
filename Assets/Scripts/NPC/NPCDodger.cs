using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NPCDodger : MonoBehaviour
{
    [SerializeField] private float dodgeSpeed;
    [SerializeField] private float dodgeDistance;
    private Player player;
    private Rigidbody2D rigidBody;
    private float totalDistance;
    private Vector3 dodgeDirection;
    public bool IsDodging { get; private set; }
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }
    public void Dodge()
    {
        var targetDirection = player.transform.position - transform.position;
        dodgeDirection = Vector3.Cross(targetDirection, Vector3.forward).normalized;
        if (Random.value > 0.5f)
            dodgeDirection *= -1;
        totalDistance = 0f;
        IsDodging = true;
    }
    private void Update()
    {
        if(!IsDodging)
            return;
        
        var positionThisFrame = dodgeSpeed * Time.fixedDeltaTime * (Vector2)dodgeDirection;
        rigidBody.MovePosition(rigidBody.position + positionThisFrame);
        totalDistance += positionThisFrame.magnitude;
        if (totalDistance >= dodgeDistance)
            IsDodging = false;
    }
}