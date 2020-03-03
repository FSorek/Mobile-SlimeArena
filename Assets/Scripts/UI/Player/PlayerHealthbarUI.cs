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
    private Player player;

    private void Awake()
    {
        healthImages = GetComponentsInChildren<Image>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        player.Health.OnTakeDamage += PlayerOnTakeDamage;
    }

    private void PlayerOnTakeDamage(int damage)
    {
        if(player.Health.CurrentHealth <= healthImages.Length - 1)
            healthImages[player.Health.CurrentHealth].sprite = inactiveHealth;
    }
}