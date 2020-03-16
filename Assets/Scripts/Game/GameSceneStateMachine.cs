using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneStateMachine : MonoBehaviour
{
    [SerializeField] private int bossStagePointsFrequency = 5;
    [SerializeField] private GameSceneStateFlowData[] gameStateFlowData;
    private GameStateMachine gameStateMachine;

    private readonly StateMachine stateMachine = new StateMachine();

    public static IState CurrentGameState { get; private set; }

    private void Awake()
    {
        gameStateMachine = FindObjectOfType<GameStateMachine>();
        stateMachine.OnStateChanged += StateMachineOnStateChanged;

        CreateStageTransitionFlow(bossStagePointsFrequency);
    }

    private void CreateStageTransitionFlow(int bossStageFrequency)
    {
        for (int i = 0; i < gameStateFlowData.Length; i++)
        {
            int stageNumber = i + 1;
            GameRegularStage gameStage = gameStateFlowData[i].GetGameStage();
            GameBossCinematic bossCinematic = gameStateFlowData[i].GetBossCinematic();
            GameBossFight bossStage = gameStateFlowData[i].GetBossStage();

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
                var nextStage = gameStateFlowData[stageNumber].GetGameStage();
                stateMachine.CreateTransition(
                    bossStage,
                    nextStage,
                    () => bossStage.IsBossKilled);
            }
        }
        stateMachine.SetState(gameStateFlowData[0].GetGameStage());
    }

    private void StateMachineOnStateChanged(IState state)
    {
        CurrentGameState = state;
    }

    private void Update()
    {
        if(gameStateMachine.CurrentState is Playing)
            stateMachine.Tick();
    }
}