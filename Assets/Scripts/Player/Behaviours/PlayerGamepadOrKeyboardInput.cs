using System;
using UnityEngine;

public class PlayerGamepadOrKeyboardInput : IPlayerInput
{
    public event Action OnPrimaryAction = delegate {  };
    public Vector2 MoveVector { get; private set; }
    public Vector2 AttackDirection { get; private set; }
    public void Tick()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        MoveVector = new Vector2(horizontalInput, verticalInput);
        if (MoveVector != Vector2.zero)
            AttackDirection = MoveVector;

        if (Input.GetButtonDown("Fire1"))
            OnPrimaryAction();
    }
}