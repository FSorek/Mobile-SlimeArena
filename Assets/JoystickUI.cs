using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickUI : MonoBehaviour
{
    [SerializeField] private Image knob;
    [SerializeField] private Image background;
    private Player player;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        player.PlayerInput.OnTouchBegan += PlayerInputOnOnTouchBegan;
        player.PlayerInput.OnTouchMoved += PlayerInputOnOnTouchMoved;
        player.PlayerInput.OnTouchFinished += PlayerInputOnOnTouchFinished;
        SetVisibility(false);
    }
    
    private void PlayerInputOnOnTouchBegan(Touch touch)
    {
        transform.position = touch.position;
        SetVisibility(true);
    }
    
    private void PlayerInputOnOnTouchMoved(Touch touch)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(background.rectTransform,
            touch.position,
            null,
            out var inRectanglePosition)) 
            return;
        
        var sizeDelta = background.rectTransform.sizeDelta;
        inRectanglePosition.x /= sizeDelta.x;
        inRectanglePosition.y /= sizeDelta.y;
            
        if(inRectanglePosition.magnitude > 1f)
            inRectanglePosition.Normalize();
        var knobPosition = new Vector2(
            inRectanglePosition.x * (sizeDelta.x/3), 
            inRectanglePosition.y * (sizeDelta.y/3));
        knob.rectTransform.anchoredPosition = knobPosition;
    }
    
    private void PlayerInputOnOnTouchFinished(Touch touch)
    {
        SetVisibility(false);
    }

    private void SetVisibility(bool visible)
    {
        knob.enabled = visible;
        background.enabled = visible;
    }
}
