using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathscreenUI : MonoBehaviour
{
    [SerializeField] private GameObject deathscreen;
    private GameStateMachine gameStateMachine;

    private void Awake()
    {
        gameStateMachine = FindObjectOfType<GameStateMachine>();
        deathscreen.SetActive(false);
    }

    private void Update()
    {
        if(gameStateMachine.CurrentState is GameOver && !deathscreen.activeSelf)
            deathscreen.SetActive(true);
    }
}
