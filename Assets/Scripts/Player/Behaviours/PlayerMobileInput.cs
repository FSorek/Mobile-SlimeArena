using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMobileInput : IPlayerInput
{
    public event Action OnPrimaryAction = delegate {  };
    private Joystick movementJoystick;
    public Vector2 MoveVector { get; private set; }
    public Vector2 AttackDirection => MoveVector != Vector2.zero ? MoveVector : AttackDirection;
    public void RegisterJoystick(Joystick joy)
    {
        movementJoystick = joy;
    }
    public void Tick()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.tapCount == 1)
                OnPrimaryAction();
        }

        if (movementJoystick != null)
        {
            MoveVector = movementJoystick.Direction;
        }
    }
}