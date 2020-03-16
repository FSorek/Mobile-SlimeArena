using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartscreenUI : MonoBehaviour
{
    [SerializeField] private GameObject startscreen;
    private GameStateMachine gameStateMachine;

    private void GameStateChanged(IState state)
    {
        if(state is Playing && startscreen.activeSelf)
            startscreen.SetActive(false);
    }

    private void OnEnable()
    {
        gameStateMachine = FindObjectOfType<GameStateMachine>();
        
        if(gameStateMachine.CurrentState is PrePlay)
            startscreen.SetActive(true);
        gameStateMachine.OnGameStateChanged += GameStateChanged;
    }

    private void OnDisable()
    {
        gameStateMachine.OnGameStateChanged -= GameStateChanged;
    }
}
