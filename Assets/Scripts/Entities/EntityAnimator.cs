using UnityEngine;
public abstract class EntityAnimator<T> : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer renderer;
    protected T owner;

    protected virtual void Awake()
    {
        owner = GetComponent<T>();
    }
}