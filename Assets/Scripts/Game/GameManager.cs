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
        player.Health.OnDeath += (source) => SetGamePaused(true);
        SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
    }

    private void Update()
    {
        if(!player.PlayerInput.MenuAcceptAction ||
            !gamePaused ||
           (operation != null && !operation.isDone))
            return;
        if(!player.Health.IsDead)
            SetGamePaused(false);
        else
            operation = SceneManager.LoadSceneAsync(0);
    }

    private void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        SetGamePaused(true);
    }

    private void SetGamePaused(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f;
        gamePaused = paused;
        OnPauseStateChanged(paused);
    }
}