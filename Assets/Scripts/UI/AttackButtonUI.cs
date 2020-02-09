using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonUI : MonoBehaviour
{
    private Player player;
    private Vector2 lastMoveVector;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void Attack()
    {
        if(player.PlayerAbility.IsUsingAbility)
            return;

        var attackDirection = player.PlayerInput.LastDirection;
        player.PlayerAttack.Attack(attackDirection);
    }
}
