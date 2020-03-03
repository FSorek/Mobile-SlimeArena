using UnityEngine;

public abstract class EntitySound<T> : MonoBehaviour
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