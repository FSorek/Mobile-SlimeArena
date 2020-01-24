using UnityEngine;

public abstract class EventParticlePlayer<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected ParticleSystem particle;
    [SerializeField] protected T listenedComponent;
    
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