using UnityEngine;
[RequireComponent(typeof(Player))]

public class PlayerAnimation : EntityAnimator<Player>
{
    [SerializeField] private Transform hands;
    [SerializeField] private Vector2 handsAttackDirectionOffset;
    private Vector2 initialPosition;
    private PlayerAbility playerAbility;

    protected override void Awake()
    {
        base.Awake();
        var playerAttack = GetComponent<PlayerAttack>();
        playerAttack.OnAttack += PlayerOnAttack;
        initialPosition = hands.transform.localPosition;
        playerAbility = owner.GetComponent<PlayerAbility>();
    }

    private void PlayerOnAttack(Vector2 direction)
    {
        //var rotationPosition = (Vector2)rightHand.position + direction;
        hands.rotation = Quaternion.FromToRotation(Vector2.up, direction);
        hands.localPosition = initialPosition + direction * handsAttackDirectionOffset;
        animator.SetTrigger("Attack");
    }

    protected override void Tick()
    {
        animator.SetBool("IsAbilityActive", playerAbility.IsUsingAbility);
        if(playerAbility.IsUsingAbility)
            return;
        
        animator.SetBool("IsMoving", owner.IsMoving);
        spriteRenderer.flipX = owner.PlayerInput.MoveVector.x < 0;
    }
}