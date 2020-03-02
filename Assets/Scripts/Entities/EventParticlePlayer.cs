using System;
using UnityEngine;

public abstract class EventParticlePlayer<T> : MonoBehaviour
{
    [SerializeField] protected ParticleSystem particle;
    protected T ListenedComponent { get; private set; }

    private void Awake()
    {
        ListenedComponent = GetComponent<T>();
    }

    private void Start()
    {
        Subscribe();
    }

    protected abstract void Subscribe();

    protected void ListenedComponentEventFired()
    {
        particle.Play();
    }
}