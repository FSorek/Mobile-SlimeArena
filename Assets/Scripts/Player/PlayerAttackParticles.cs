using UnityEngine;

public class PlayerAttackParticles : EventParticlePlayer<PlayerAttack>
{
    protected override void Subscribe()
    {
        listenedComponent.OnAttack += (direction) =>
        {
            Debug.Log("FIRE");
            ListenedComponentEventFired();
        };
    }
}