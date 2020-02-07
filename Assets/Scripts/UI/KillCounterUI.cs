using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killNumberText;
    private PlayerScoreTracker playerPlayerScoreTracker;
    private int lastScore;

    private void Start()
    {
        playerPlayerScoreTracker = FindObjectOfType<Player>().PlayerScoreTracker;
    }

    private void Update()
    {
        if (lastScore != playerPlayerScoreTracker.Score)
        {
            lastScore = playerPlayerScoreTracker.Score;
            killNumberText.text = $"{lastScore}";
        }
    }
}
