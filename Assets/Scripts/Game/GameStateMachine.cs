using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField] private int bossStagePointsFrequency = 5;
    [SerializeField] private GameStateFlowData[] gameStateFlowData;

    private readonly StateMachine stateMachine = new StateMachine();

    public static IState CurrentGameState { get; private set; }

    private void Awake()
    {
        stateMachine.OnStateChanged += StateMachineOnStateChanged;

        CreateStageTransitionFlow(bossStagePointsFrequency);
    }

    private void CreateStageTransitionFlow(int bossStageFrequency)
    {
        for (int i = 0; i < gameStateFlowData.Length; i++)
        {
            int stageNumber = i + 1;
            GameRegularStage gameStage = gameStateFlowData[i].GameStage;
            GameBossCinematic bossCinematic = gameStateFlowData[i].BossCinematic;
            GameBossFight bossStage = gameStateFlowData[i].BossStage;

            stateMachine.CreateTransition(
                gameStage,
                bossCinematic,
                () => ScoreTracker.Score >= bossStageFrequency * stageNumber);
            
            stateMachine.CreateTransition(
                bossCinematic,
                bossStage,
                () => bossCinematic.IsCinematicFinished);

            if (stageNumber < gameStateFlowData.Length)
            {
                var nextStage = gameStateFlowData[stageNumber].GameStage;
                stateMachine.CreateTransition(
                    bossStage,
                    nextStage,
                    () => bossStage.IsBossKilled);
            }
        }
        stateMachine.SetState(gameStateFlowData[0].GameStage);
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

public class GameMenuState : IState
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