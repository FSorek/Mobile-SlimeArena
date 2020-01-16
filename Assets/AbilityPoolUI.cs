using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPoolUI : MonoBehaviour
{
    private Image poolImage;
    private double lastPoolPercentage;
    private PlayerAbility playerAbility;

    private void Awake()
    {
        poolImage = GetComponent<Image>();
        playerAbility = FindObjectOfType<PlayerAbility>();
    }

    private void Update()
    {
        if (lastPoolPercentage != playerAbility.CurrentPoolPercentage)
        {
            lastPoolPercentage = playerAbility.CurrentPoolPercentage;
            poolImage.fillAmount = (float)lastPoolPercentage;
        }
    }
}
