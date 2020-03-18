using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileInput : IPlayerInput
{
    private Joystick movementJoystick;
    public bool MenuAcceptAction { get; private set; }
    public bool PrimaryActionDown { get; private set; }
    public bool SecondaryActionDown { get; private set; }
    public bool SecondaryActionUp { get; private set; }
    public Vector2 MoveVector { get; private set; }
    public Vector2 AttackDirection { get; private set; }
    private float lastActionTime;
    public void RegisterJoystick(Joystick joy)
    {
        movementJoystick = joy;
    }

    public void Tick()
    {
        if (movementJoystick != null)
        {
            MoveVector = movementJoystick.Direction.normalized;
            if (MoveVector != Vector2.zero)
                AttackDirection = MoveVector;
        }
        if (lastActionTime != Time.time)
        {
            if (MenuAcceptAction) MenuAcceptAction = false;
            if (PrimaryActionDown) PrimaryActionDown = false;
            if (SecondaryActionDown) SecondaryActionDown = false;
            if (SecondaryActionUp) SecondaryActionUp = false;
        }
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if(touch.tapCount == 1)
                MenuAcceptAction = true;
        }
    }

    public void FirePrimaryActionDown()
    {
        PrimaryActionDown = true;
        lastActionTime = Time.time;
    }

    public void FireSecondaryActionDown()
    {
        SecondaryActionDown = true;
        lastActionTime = Time.time;
    }

    public void FireSecondaryActionUp()
    {
        SecondaryActionUp = true;
        lastActionTime = Time.time;
    }
}