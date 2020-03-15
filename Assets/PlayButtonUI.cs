using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayButtonUI : MonoBehaviour
{
    public static bool Pressed { get; set; }
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => Pressed = true);
    }
}
