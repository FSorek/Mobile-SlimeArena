using UnityEngine;
[RequireComponent(typeof(Player))]

public class PlayerAnimation : EntityAnimator<Player>
{
    [SerializeField] private Transform hands;
    [SerializeField] private Vector2 handsAttackDirectionOffset;
    private Vector2 initialPosition;
    private PlayerAbility playerAbility;
    private PlayerAttack playerAttack;

    protected override void Awake()
    {
        base.Awake();
        playerAttack = GetComponent<PlayerAttack>();
        playerAttack.OnAttack += PlayerOnAttack;
        initialPosition = hands.transform.localPosition;
        playerAbility = owner.GetComponent<PlayerAbility>();
    }

    private void PlayerOnAttack(Vector2 direction)
    {
        animator.SetTrigger("Attack");
    }

    protected override void Tick()
    {
        hands.rotation = Quaternion.FromToRotation(Vector2.up, playerAttack.LastDirection);
        hands.localPosition = initialPosition + playerAttack.LastDirection * handsAttackDirectionOffset;
        
        animator.SetBool("IsAbilityActive", playerAbility.IsUsingAbility);
        if(playerAbility.IsUsingAbility)
            return;
        
        animator.SetBool("IsMoving", owner.IsMoving);
        spriteRenderer.flipX = owner.PlayerInput.MoveVector.x < 0;
    }
}