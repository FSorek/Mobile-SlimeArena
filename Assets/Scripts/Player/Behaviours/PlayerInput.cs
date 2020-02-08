using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput
{
    private Joystick movementJoystick;
    private Vector2 moveVector;
    private Vector2 lastDirection;
    public Vector2 MoveVector => moveVector;
    public Vector2 LastDirection => lastDirection;
    public void RegisterJoystick(Joystick joy)
    {
        movementJoystick = joy;
    }
    public void Tick()
    {
        if(movementJoystick == null) 
            return;
        moveVector = movementJoystick.Direction;
        if (movementJoystick.Direction != Vector2.zero)
            lastDirection = movementJoystick.Direction;
    }
}