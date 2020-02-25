using System;
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
        
        var stageOne = new GameStageOne(slimePool, 5f);
        var bossStartCinematic = new GameStartBossCinematic(playableDirector, bossPool, bossSpawnPosition.position);
        var bossFight = new GameBossFight();
        
        stateMachine.OnStateChanged += StateMachineOnStateChanged;

        stateMachine.CreateTransition(
            stageOne,
            bossStartCinematic,
            () => ScoreTracker.Score >= 1);
        
        stateMachine.CreateTransition(
            bossStartCinematic,
            bossFight,
            () => bossStartCinematic.IsCinematicFinished);

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