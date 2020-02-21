using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackDirectionUI : MonoBehaviour
{
    [SerializeField]private Joystick attackJoystick;
    private Player player;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if(player.PlayerAbility.IsUsingAbility)
            return;
        
        if(attackJoystick.Direction.magnitude > 0)
            player.PlayerAttack.Attack();
    }
}
