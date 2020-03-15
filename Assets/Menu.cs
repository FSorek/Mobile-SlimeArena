using UnityEngine.SceneManagement;

public class Menu : IState
{
    public void StateEnter()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        PlayButtonUI.Pressed = false;
    }

    public void ListenToState()
    {
        
    }

    public void StateExit()
    {
        
    }
}


