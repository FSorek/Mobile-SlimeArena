using UnityEngine;

internal class InputMovement : IState
{
    private readonly Player player;
    private readonly float moveSpeed;
    private readonly Rigidbody2D playerRbody;
    public InputMovement(Player player, float moveSpeed)
    {
        this.player = player;
        this.moveSpeed = moveSpeed;
        playerRbody = player.GetComponent<Rigidbody2D>();
    }

    public void StateEnter()
    {
        playerRbody.velocity = Vector2.zero;
    }

    public void ListenToState()
    {
        playerRbody.MovePosition(playerRbody.position + Time.fixedDeltaTime * moveSpeed * player.PlayerInput.MoveVector.normalized);
    }

    public void StateExit()
    {
        
    }
}