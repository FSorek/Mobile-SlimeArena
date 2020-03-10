using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayButtonUI : MonoBehaviour
{
    public static string LevelToLoad { get; private set; }
    
    [SerializeField] private string levelName;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => LevelToLoad = levelName);
    }
}
