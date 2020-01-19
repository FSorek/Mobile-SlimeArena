using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackDirectionUI : MonoBehaviour
{
    [SerializeField]private Joystick attackJoystick;
    private PlayerAttack player;
    private PlayerAbility playerAbility;
    private void Awake()
    {
        player = FindObjectOfType<PlayerAttack>();
        playerAbility = player.GetComponent<PlayerAbility>();
    }

    private void Update()
    {
        if(playerAbility.IsUsingAbility)
            return;
        
        if(attackJoystick.Direction.magnitude > 0)
            player.Attack(attackJoystick.Direction);
    }
}
