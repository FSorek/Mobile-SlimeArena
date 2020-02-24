using UnityEngine;
using UnityEngine.Playables;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField] private ObjectPool slimePool;
    [SerializeField] private ObjectPool skeletonPool;
    
    [SerializeField] private ObjectPool bossPool;
    [SerializeField] private Transform bossSpawnPosition;

    private readonly StateMachine stateMachine = new StateMachine();

    private void Awake()
    {
        var playableDirector = FindObjectOfType<PlayableDirector>();
        
        var stageOne = new GameStageOneState(slimePool, 5f);
        var bossCinematic = new GameBossCinematicState(playableDirector, bossPool, bossSpawnPosition.position);
        var bossFight = new GameBossFightState();
        
        stateMachine.CreateTransition(
            stageOne,
            bossCinematic,
            () => ScoreTracker.Score == 1);
        
        stateMachine.CreateTransition(
            bossCinematic,
            bossFight,
            () => bossCinematic.IsCinematicFinished);

        stateMachine.SetState(stageOne);
    }

    private void Update()
    {
        stateMachine.Tick();
    }
}

public class GameBossFightState : IState
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