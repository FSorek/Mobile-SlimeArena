using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killNumberText;
    private ScoreTracker playerScoreTracker;
    private int lastScore;

    private void Start()
    {
        playerScoreTracker = FindObjectOfType<Player>().ScoreTracker;
    }

    private void Update()
    {
        if (lastScore != playerScoreTracker.Score)
        {
            lastScore = playerScoreTracker.Score;
            killNumberText.text = $"{lastScore}";
        }
    }
}
