using System;
using UnityEngine;

public class PlayerGamepadOrKeyboardInput : IPlayerInput
{
    public bool PrimaryActionDown => Input.GetButtonDown("Fire1");
    public Vector2 MoveVector { get; private set; }
    public Vector2 AttackDirection { get; private set; }
    public void Tick()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        MoveVector = new Vector2(horizontalInput, verticalInput);
        if (MoveVector != Vector2.zero)
            AttackDirection = MoveVector;
    }
}