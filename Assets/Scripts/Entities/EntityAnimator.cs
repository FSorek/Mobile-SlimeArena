using UnityEngine;
public abstract class EntityAnimator<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected T owner;

    protected virtual void Awake()
    {
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        owner = GetComponent<T>();
    }

    private void Update()
    {
        Tick();
    }

    protected abstract void Tick();
}