using UnityEngine;

public class AttackButtonUI : MonoBehaviour
{
    private Player player;
    private PlayerMobileInput mobileInput;
    private Vector2 lastMoveVector;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        mobileInput = player.PlayerInput as PlayerMobileInput;
    }

    public void Attack()
    {
        if(mobileInput == null || player.PlayerAbility.IsUsingAbility)
            return;
        mobileInput.FirePrimaryAction();
    }
}
