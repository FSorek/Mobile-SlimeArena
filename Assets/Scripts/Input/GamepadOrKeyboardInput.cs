using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GamepadOrKeyboardInput : IPlayerInput
{
    public bool MenuAcceptAction => Input.GetButtonDown("Fire1");
    public bool PrimaryActionDown => Input.GetButtonDown("Fire1");
    public bool SecondaryActionDown => Input.GetButtonDown("Fire2");
    public bool SecondaryActionUp => Input.GetButtonUp("Fire2");
    public Vector2 MoveVector { get; private set; }
    public Vector2 AttackDirection { get; private set; }
    public void Tick()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        MoveVector = new Vector2(horizontalInput, verticalInput).normalized;

            AttackDirection = MoveVector;
    }
}