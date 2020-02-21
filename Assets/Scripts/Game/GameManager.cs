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
        player.PlayerInput.OnPrimaryAction += PlayerInputOnPrimaryAction;
        SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
    }

    private void PlayerInputOnPrimaryAction()
    {
        if(!gamePaused ||
           (operation != null && !operation.isDone))
            return;
        if(!player.IsDead)
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