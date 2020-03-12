using UnityEngine;

public class AttackButtonUI : MonoBehaviour
{
    private Player player;
    private PlayerEntityStateMachine playerStateMachine;
    private MobileInput mobileInput;
    private Vector2 lastMoveVector;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        mobileInput = PlayerInputManager.CurrentInput as MobileInput;
    }

    public void Attack()
    {
        if(mobileInput == null)
            return;
        mobileInput.FirePrimaryActionDown();
    }
}
