using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameStateMachine : MonoBehaviour
{
    public Action<IState> OnGameStateChanged = delegate {  };
    private StateMachine stateMachine = new StateMachine();
    private static bool initialized;
    public IState CurrentState => stateMachine.CurrentState;
    
    private void Awake()
    {
        if (initialized)
        {
            Destroy(gameObject);
            return;
        }
        initialized = true;
        DontDestroyOnLoad(gameObject);
        
        stateMachine.OnStateChanged += (state) => OnGameStateChanged(state);
        
        var menu = new Menu();
        var loading = new LoadingLevel();
        var paused = new Paused();
        var prePlay = new PrePlay();
        var playing = new Playing();
        var gameOver = new GameOver();
        
        stateMachine.CreateTransition(
            menu,
            loading,
            () => PlayButtonUI.Pressed);
        
        stateMachine.CreateTransition(
            loading,
            prePlay,
            loading.IsLoadingFinished);
        
        stateMachine.CreateTransition(
            prePlay,
            playing,
            () => PlayerInputManager.CurrentInput.MenuAcceptAction);
        
        stateMachine.CreateTransition(
            playing,
            paused,
            () => PauseButton.Pressed);
        
        stateMachine.CreateTransition(
            paused,
            playing,
            () => PauseButton.Pressed);
        
        stateMachine.CreateTransition(
            playing,
            gameOver,
            () => playing.IsGameFinished);

        stateMachine.SetState(menu);
    }

    private void Update()
    {
        stateMachine.Tick();
    }
}

public class GameOver : IState
{
    public void StateEnter()
    {
        
    }

    public void ListenToState()
    {
        
    }

    public void StateExit()
    {
        
    }
}