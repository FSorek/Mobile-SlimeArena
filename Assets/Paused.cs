using UnityEngine;

public class Paused : IState
{
    public static bool IsPaused { get; private set; }
    public void StateEnter()
    {
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void ListenToState()
    {
        
    }

    public void StateExit()
    {
        Time.timeScale = 1f;
        IsPaused = false;
    }
}