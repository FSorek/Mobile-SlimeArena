using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPoolUI : MonoBehaviour
{
    private Image poolImage;
    private double lastPoolPercentage;
    private Player player;

    private void Awake()
    {
        poolImage = GetComponent<Image>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (lastPoolPercentage != player.PlayerAbility.CurrentPoolPercentage)
        {
            lastPoolPercentage = player.PlayerAbility.CurrentPoolPercentage;
            poolImage.fillAmount = (float)lastPoolPercentage;
        }
    }
}
