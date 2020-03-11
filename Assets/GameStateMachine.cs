using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        stateMachine.OnStateChanged += (state) => OnGameStateChanged(state);
        
        var menu = new Menu();
        var loading = new LoadingLevel();
        var paused = new Paused();
        var prePlay = new PrePlay();
        var playing = new Playing();
        
        stateMachine.CreateTransition(
            loading,
            prePlay,
            loading.IsLoadingFinished);

        stateMachine.SetState(loading);
    }
}

public class Menu : IState
{
    public void StateEnter()
    {
        throw new NotImplementedException();
    }

    public void ListenToState()
    {
        throw new NotImplementedException();
    }

    public void StateExit()
    {
        throw new NotImplementedException();
    }
}

public class LoadingLevel : IState
{
    private AsyncOperation operation;
    public bool IsLoadingFinished() => operation.isDone;
    public void StateEnter()
    {
        operation = SceneManager.LoadSceneAsync("GameScene");
    }

    public void ListenToState()
    {
        
    }

    public void StateExit()
    {
        
    }
}

public class Paused : IState
{
    public static bool IsPaused { get; private set; }
    public void StateEnter()
    {
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void ListenToState()
    {
        
    }

    public void StateExit()
    {
        Time.timeScale = 1f;
        IsPaused = false;
    }
}

public class Playing : IState
{
    public void StateEnter()
    {
        
    }

    public void ListenToState()
    {
        throw new NotImplementedException();
    }

    public void StateExit()
    {
        throw new NotImplementedException();
    }
}

public class PrePlay : IState
{
    private Player player;
    public bool CanContinue() => player.PlayerInput.PrimaryActionDown;
    public void StateEnter()
    {
        player = Object.FindObjectOfType<Player>();
    }

    public void ListenToState()
    {
        
    }

    public void StateExit()
    {
        
    }
}

