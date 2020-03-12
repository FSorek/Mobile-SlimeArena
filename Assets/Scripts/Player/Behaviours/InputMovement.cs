using UnityEngine;

internal class InputMovement : IState
{
    private readonly float moveSpeed;
    private readonly Rigidbody2D playerRbody;
    public InputMovement(Player player, float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        playerRbody = player.GetComponent<Rigidbody2D>();
    }

    public void StateEnter()
    {
        playerRbody.velocity = Vector2.zero;
    }

    public void ListenToState()
    {
        playerRbody.MovePosition(playerRbody.position + Time.fixedDeltaTime * moveSpeed * PlayerInputManager.CurrentInput.MoveVector.normalized);
    }

    public void StateExit()
    {
        
    }
}