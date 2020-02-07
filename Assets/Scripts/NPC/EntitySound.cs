using UnityEngine;

public abstract class EntitySound<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected AudioSource audioSource;
    protected T owner;

    protected abstract void Subscribe();
    private void Awake()
    {
        owner = GetComponent<T>();
    }

    private void Start()
    {
        Subscribe();
    }
}