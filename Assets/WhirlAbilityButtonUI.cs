using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WhirlAbilityButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button button;
    private PlayerAbility playerAbility;

    private void Awake()
    {
        playerAbility = FindObjectOfType<PlayerAbility>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        playerAbility.UseAbility();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerAbility.StopAbility();
    }
}
