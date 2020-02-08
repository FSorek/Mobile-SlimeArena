using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonUI : MonoBehaviour
{
    [SerializeField]private Button attackButton;
    private Player player;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void Attack()
    {
        if(player.PlayerAbility.IsUsingAbility)
            return;

        player.PlayerAttack.Attack(player.PlayerInput.MoveVector);
    }
}
