using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDirectionUI : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    private void Start()
    {
        FindObjectOfType<Player>().PlayerInput.RegisterJoystick(joystick);
    }
}
