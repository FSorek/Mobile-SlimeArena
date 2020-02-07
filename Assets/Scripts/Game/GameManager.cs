using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event Action<bool> OnPauseStateChanged = delegate {  };

    private static GameManager instance;
    private bool gamePaused;
    private Player player;
    private AsyncOperation operation;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        player.OnDeath += () => SetGamePaused(true);
        SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
    }

    private void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        SetGamePaused(true);
    }

    private void Update()
    {
        if(!gamePaused || 
           Input.touchCount <= 0 ||
           (operation != null && !operation.isDone))
            return;
        
        var touch = Input.GetTouch(0);
        if(!player.IsDead && touch.tapCount == 1)
            SetGamePaused(false);
        else if(player.IsDead && touch.tapCount >= 2)
        {
            operation = SceneManager.LoadSceneAsync(0);
        } 
    }
    private void SetGamePaused(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f;
        gamePaused = paused;
        OnPauseStateChanged(paused);
    }
}