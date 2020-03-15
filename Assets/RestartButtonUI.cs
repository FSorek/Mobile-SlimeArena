using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButtonUI : MonoBehaviour
{
    public static bool Pressed { get; set; }
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => Pressed = true);
    }
}
