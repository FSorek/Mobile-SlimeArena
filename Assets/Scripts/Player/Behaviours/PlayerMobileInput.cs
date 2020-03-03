using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMobileInput : IPlayerInput
{
    private Joystick movementJoystick;
    public bool PrimaryActionDown { get; private set; }
    public bool SecondaryActionDown { get; private set; }
    public bool SecondaryActionUp { get; private set; }
    public Vector2 MoveVector { get; private set; }
    public Vector2 AttackDirection => MoveVector != Vector2.zero ? MoveVector : AttackDirection;
    public void RegisterJoystick(Joystick joy)
    {
        movementJoystick = joy;
    }

    public void Tick()
    {
        if (movementJoystick != null)
        {
            MoveVector = movementJoystick.Direction;
        }

        if (PrimaryActionDown) PrimaryActionDown = false;
        if (SecondaryActionDown) SecondaryActionDown = false;
        if (SecondaryActionUp) SecondaryActionUp = false;
    }

    public void FirePrimaryActionDown()
    {
        PrimaryActionDown = true;
    }

    public void FireSecondaryActionDown()
    {
        SecondaryActionDown = true;
    }

    public void FireSecondaryActionUp()
    {
        SecondaryActionUp = true;
    }
}