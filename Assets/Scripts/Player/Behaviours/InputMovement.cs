using UnityEngine;

internal class InputMovement : IMovement
{
    private readonly Player player;
    private readonly float moveSpeed;
    private readonly Rigidbody2D playerRbody;
    private bool isMoving;

    public bool IsMoving => isMoving;
    public InputMovement(Player player, float moveSpeed)
    {
        this.player = player;
        this.moveSpeed = moveSpeed;
        playerRbody = player.GetComponent<Rigidbody2D>();
    }


    public void Initialize()
    {
        playerRbody.velocity = Vector2.zero;
    }
    public void Move(float speedPercentage = 1f)
    {
        if (player.PlayerInput.MoveVector.magnitude > 0)
        {
            playerRbody.MovePosition(playerRbody.position + Time.fixedDeltaTime * moveSpeed * speedPercentage * player.PlayerInput.MoveVector.normalized);
            if (!isMoving) 
                isMoving = true;
        }
        else if(isMoving)
            isMoving = false;
    }
}