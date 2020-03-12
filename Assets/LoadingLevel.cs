using UnityEngine;
using UnityEngine.SceneManagement;

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