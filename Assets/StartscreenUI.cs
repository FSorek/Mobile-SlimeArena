using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartscreenUI : MonoBehaviour
{
    [SerializeField] private GameObject startscreen;
    private GameManager gameManager;
    private Player player;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        gameManager.OnPauseStateChanged += GameManagerOnPauseStateChanged;
    }
    private void Start()
    {
        startscreen.SetActive(true);
    }
    private void GameManagerOnPauseStateChanged(bool paused)
    {
        if(!player.IsDead && startscreen != null)
            startscreen.SetActive(paused);
    }
  
}
