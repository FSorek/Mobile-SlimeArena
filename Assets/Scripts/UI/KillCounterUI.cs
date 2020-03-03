using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killNumberText;
    private void Start()
    {
        ScoreTracker.OnScoreIncreased += (score) => killNumberText.text = $"{score}";;
    }
}
