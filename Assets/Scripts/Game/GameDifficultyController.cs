using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameDifficultyController : MonoBehaviour
{
    [SerializeField] private Spawner slimeSpawner;
    [SerializeField] private Spawner skeletonSpawner;
    private readonly Dictionary<int, Action> scoreBasedEvents = new Dictionary<int, Action>();
    private void Awake()
    {
        Instantiate(skeletonSpawner);
    }

    private void Start()
    {
        var player = FindObjectOfType<Player>();
        player.PlayerScoreTracker.OnScoreIncreased += PlayerOnScoreIncreased;
        
        scoreBasedEvents.Add(15, () => Instantiate(skeletonSpawner));
        scoreBasedEvents.Add(25, () => Instantiate(slimeSpawner));
    }

    private void PlayerOnScoreIncreased(int score)
    {
        if (scoreBasedEvents.ContainsKey(score))
        {
            scoreBasedEvents[score].Invoke();
        }
    }
}
