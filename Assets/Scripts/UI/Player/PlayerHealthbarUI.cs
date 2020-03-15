using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbarUI : MonoBehaviour
{
    [SerializeField] private Sprite activeHealth;
    [SerializeField] private Sprite inactiveHealth;
    private Image[] healthImages;
    private ITakeDamage playerHealth;

    private void Awake()
    {
        healthImages = GetComponentsInChildren<Image>();
        playerHealth = FindObjectOfType<Player>().GetComponent<ITakeDamage>();
    }

    private void Start()
    {
        playerHealth.OnTakeDamage += PlayerOnTakeDamage;
        if(playerHealth is ICanRestore restorableHealth)
            restorableHealth.OnRestore += PlayerOnRestoreHealth;
    }

    private void PlayerOnRestoreHealth(int amount)
    {
        healthImages[playerHealth.CurrentHealth - 1].sprite = activeHealth;
    }

    private void PlayerOnTakeDamage(int damage)
    {
        if(playerHealth.CurrentHealth <= healthImages.Length - 1)
            healthImages[playerHealth.CurrentHealth].sprite = inactiveHealth;
    }
}