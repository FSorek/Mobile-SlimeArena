using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField] private ObjectPool slimePool;
    [SerializeField] private ObjectPool skeletonPool;
    
    [SerializeField] private ObjectPool bossPool;
    [SerializeField] private Transform bossSpawnPosition;

    private readonly StateMachine stateMachine = new StateMachine();

    public static IState CurrentGameState { get; private set; }

    private void Awake()
    {
        var playableDirector = FindObjectOfType<PlayableDirector>();
        
        var slimeSpawner = new Spawner(slimePool);
        var skeletonSpawner = new Spawner(skeletonPool);
        
        var stageOneSpawners = new List<(float, Spawner)>();
        stageOneSpawners.Add((5f, slimeSpawner));
        
        var stageTwoSpawners = new List<(float, Spawner)>();
        stageTwoSpawners.Add((5f, slimeSpawner));
        stageTwoSpawners.Add((8f, skeletonSpawner));
        
        var loading = new GameLoading();
        var started = new GameStarted();
        var stageOne = new GameRegularStage(stageOneSpawners);
        var bossStartCinematic = new GameBossCinematic(playableDirector, bossPool, bossSpawnPosition.position);
        var bossFight = new GameBossFight();
        var stageTwo = new GameRegularStage(stageTwoSpawners);
        
        stateMachine.OnStateChanged += StateMachineOnStateChanged;

        stateMachine.CreateTransition(
            stageOne,
            bossStartCinematic,
            () => ScoreTracker.Score >= 1);
        
        stateMachine.CreateTransition(
            bossStartCinematic,
            bossFight,
            () => bossStartCinematic.IsCinematicFinished);
        
        stateMachine.CreateTransition(
            bossFight,
            stageTwo,
            () => bossFight.IsBossKilled);

        stateMachine.SetState(stageOne);
    }

    private void StateMachineOnStateChanged(IState state)
    {
        CurrentGameState = state;
    }

    private void Update()
    {
        stateMachine.Tick();
    }
}

public class GameLoading : IState
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
 
public class GameStarted : IState
{
    public void StateEnter()
    {
        Time.timeScale = 0f;
    }

    public void ListenToState()
    {
        
    }

    public void StateExit()
    {
        Time.timeScale = 1f;
    }
}