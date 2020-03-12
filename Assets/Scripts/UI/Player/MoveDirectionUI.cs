using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDirectionUI : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    private void Start()
    {
        var mobileInput = PlayerInputManager.CurrentInput as MobileInput;
        mobileInput?.RegisterJoystick(joystick);
    }
}
