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

public class GameRegularStage : IState
{
    private readonly List<(float, Spawner)> timerSpawners;
    private float lastSpawnTime;
    public GameRegularStage(List<(float, Spawner)> timerSpawners)
    {
        this.timerSpawners = timerSpawners;
    }
    public void StateEnter()
    {
    }

    public void ListenToState()
    {
        foreach (var timerSpawner in timerSpawners)
        {
            if(timerSpawner.Item2.IsSpawning 
               || Time.time - timerSpawner.Item2.LastSpawnTime < timerSpawner.Item1)
                return;

            timerSpawner.Item2.OrderSpawn();
            lastSpawnTime = Time.time;
        }
    }

    public void StateExit()
    {
    }
}