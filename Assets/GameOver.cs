public class GameOver : IState
{
    public void StateEnter()
    {
        RestartButtonUI.Pressed = false;
    }

    public void ListenToState()
    {
        
    }

    public void StateExit()
    {
        
    }
}