using System;
using UnityEngine;

public class PlayerInput
{
    public event Action<Touch> OnTouchBegan = delegate { };
    public event Action<Touch> OnTouchMoved = delegate { };
    public event Action<Touch> OnTouchFinished = delegate { };

    private Vector2 moveVector;
    private Vector2 firstTouchPosition;

    public Vector2 MoveVector => moveVector;


    public void Tick()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    firstTouchPosition = touch.position;
                    OnTouchBegan(touch);
                    break;
                case TouchPhase.Moved:
                    moveVector = touch.position - firstTouchPosition;
                    OnTouchMoved(touch);
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    moveVector = Vector2.zero;
                    OnTouchFinished(touch);
                    break;
                case TouchPhase.Canceled:
                    moveVector = Vector2.zero;
                    OnTouchFinished(touch);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}