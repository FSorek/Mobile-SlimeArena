using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button button;
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        player.PlayerAbility.UseAbility();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.PlayerAbility.StopAbility();
    }
}
