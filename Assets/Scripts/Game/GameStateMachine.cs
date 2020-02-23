using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField] private ObjectPool slimePool;
    [SerializeField] private ObjectPool skeletonPool;

    private readonly StateMachine stateMachine = new StateMachine();

    private void Awake()
    {
        var stageOne = new GameStageOneState(skeletonPool, 5f);
        
        stateMachine.SetState(stageOne);
    }

    private void Update()
    {
        stateMachine.Tick();
    }
}