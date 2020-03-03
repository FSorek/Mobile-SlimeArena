using System;
using UnityEngine;
public abstract class EntityAnimator<T> : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer renderer;
    protected T owner;
    private bool startsFlipped;
    private float lastPositionX;
    protected virtual void Awake()
    {
        owner = GetComponent<T>();
        startsFlipped = renderer.flipX;
    }

    private void LateUpdate()
    {
        var currentPositionX = transform.position.x;
        if(lastPositionX == currentPositionX)
            return;
        bool shouldFlip = startsFlipped ? lastPositionX - currentPositionX > 0 : lastPositionX - currentPositionX < 0;
        if(renderer.flipX != shouldFlip)
            renderer.flipX = shouldFlip;
        lastPositionX = currentPositionX;
    }
}