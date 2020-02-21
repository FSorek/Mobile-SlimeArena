using UnityEngine;

public class AttackButtonUI : MonoBehaviour
{
    private Player player;
    private Vector2 lastMoveVector;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void Attack()
    {
        if(player.PlayerAbility.IsUsingAbility)
            return;

        player.PlayerAttack.Attack();
    }
}
