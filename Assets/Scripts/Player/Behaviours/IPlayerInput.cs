using System;
using UnityEngine;

public interface IPlayerInput
{
    event Action OnPrimaryAction;
    Vector2 MoveVector { get; }
    Vector2 AttackDirection { get; }
    void Tick();
}