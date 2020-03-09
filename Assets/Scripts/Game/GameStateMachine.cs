using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField] private Transform bossSpawnPosition;
    [SerializeField] private EnemyNPC bossPrefab;
    [SerializeField] private NpcSpawnerData[] stageOneSpawners; // move to SO

    private readonly StateMachine stateMachine = new StateMachine();

    public static IState CurrentGameState { get; private set; }

    private void Awake()
    {
        var playableDirector = FindObjectOfType<PlayableDirector>();
        var currentSpawnerSystem = NpcSpawnerSystem.Instance;
        
        var stageOne = new GameRegularStage(stageOneSpawners, currentSpawnerSystem);
        var bossStartCinematic = new GameBossCinematic(playableDirector, bossPrefab, bossSpawnPosition.position, currentSpawnerSystem);
        var bossFight = new GameBossFight();
        var stageTwo = new GameRegularStage(stageOneSpawners, currentSpawnerSystem);
        
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