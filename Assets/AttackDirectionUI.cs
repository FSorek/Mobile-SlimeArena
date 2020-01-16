using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackDirectionUI : MonoBehaviour
{
    [SerializeField]private Joystick attackJoystick;
    private PlayerAttack player;
    private void Awake()
    {
        player = FindObjectOfType<PlayerAttack>();
    }

    private void Update()
    {
        if(attackJoystick.Direction.magnitude > 0)
            player.Attack(attackJoystick.Direction);
    }
}
