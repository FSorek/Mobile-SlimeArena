using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartscreenUI : MonoBehaviour
{
    [SerializeField] private GameObject startscreen;
    private GameStateMachine gameStateMachine;

    private void Awake()
    {
        gameStateMachine = FindObjectOfType<GameStateMachine>();
        
        if(gameStateMachine.CurrentState is PrePlay)
            startscreen.SetActive(true);
        gameStateMachine.OnGameStateChanged += GameStateChanged;
    }

    private void GameStateChanged(IState state)
    {
        if(!(state is PrePlay) && startscreen.activeSelf)
            startscreen.SetActive(false);
    }
}
