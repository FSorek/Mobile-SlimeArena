using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMobileInput : IPlayerInput
{
    private Joystick movementJoystick;
    public Vector2 MoveVector { get; private set; }
    public bool PrimaryActionDown { get; private set; }
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
            PrimaryActionDown = touch.tapCount == 1;
        }

        if (movementJoystick != null)
        {
            MoveVector = movementJoystick.Direction;
        }

        if (PrimaryActionDown)
            PrimaryActionDown = false;
    }

    public void FirePrimaryAction()
    {
        PrimaryActionDown = true;
    }
}