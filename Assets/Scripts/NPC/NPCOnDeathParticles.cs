using System;
using UnityEngine;

public class NPCOnDeathParticles : EventParticlePlayer<EnemyNPC>
{
    protected override void Subscribe()
    {
        listenedComponent.OnDeath += () => ListenedComponentEventFired();
    }
}

public class ParticleEmitter : MonoBehaviour
{
    public static ParticleEmitter Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    
    
}