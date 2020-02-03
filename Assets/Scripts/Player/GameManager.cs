using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform playerSpawnPoint; 
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        player.OnDeath += () => SetGamePaused(true);
    }

    private void Update()
    {
        if(!player.IsDead)
            return;
        
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended && touch.tapCount > 1)
            {
                var loadingOperation = SceneManager.LoadSceneAsync(0);
                loadingOperation.completed += (obj) => SetGamePaused(false);
            }
        }
    }

    private void SetGamePaused(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f;
    }
}