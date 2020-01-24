using UnityEngine;

public class PlayerAttackParticles : EventParticlePlayer<Player>
{
    protected override void Subscribe()
    {
        listenedComponent.PlayerAttack.OnAttack += 
            (direction) => ListenedComponentEventFired();
    }
}