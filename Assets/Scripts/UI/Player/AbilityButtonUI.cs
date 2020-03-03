using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button button;
    private MobileInput mobileInput;

    private void Awake()
    {
        mobileInput = FindObjectOfType<Player>().PlayerInput as MobileInput;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(mobileInput == null)
            return;
        mobileInput.FireSecondaryActionDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(mobileInput == null)
            return;
        mobileInput.FireSecondaryActionUp();
    }
}
