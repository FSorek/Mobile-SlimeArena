using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

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
            GameRegularStage gameStage = new GameRegularStage(gameStateFlowData[i].SpawningData, NpcSpawnerSystem.Instance);
            GameBossCinematic bossCinematic = new GameBossCinematic(FindObjectOfType<PlayableDirector>(), gameStateFlowData[i].BossPrefab, gameStateFlowData[i].BossSpawnPosition, NpcSpawnerSystem.Instance);
            GameBossFight bossStage = new GameBossFight(NpcSpawnerSystem.Instance);
            
            if(i == 0)
                stateMachine.SetState(gameStage);

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
                GameRegularStage nextStage = new GameRegularStage(gameStateFlowData[stageNumber].SpawningData, NpcSpawnerSystem.Instance);
                stateMachine.CreateTransition(
                    bossStage,
                    nextStage,
                    () => bossStage.IsBossKilled);
            }
        }
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