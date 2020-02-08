using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput
{
    private Joystick movementJoystick;
    private Vector2 moveVector;
    public Vector2 MoveVector => moveVector;
    public void RegisterJoystick(Joystick joy)
    {
        movementJoystick = joy;
    }
    public void Tick()
    {
        if(movementJoystick == null) 
            return;
        moveVector = movementJoystick.Direction;
    }
}