using System;
using UnityEngine;

public interface IPlayerInput
{
    bool MenuAcceptAction { get; }
    bool PrimaryActionDown { get; }
    bool SecondaryActionDown { get; }
    bool SecondaryActionUp { get; }
    Vector2 MoveVector { get; }
    Vector2 AttackDirection { get; }
    void Tick();
}