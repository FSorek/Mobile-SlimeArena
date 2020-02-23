using UnityEngine;

public class PlayerAttackParticles : EventParticlePlayer<Player>
{
    protected override void Subscribe()
    {
        ListenedComponent.PlayerAttack.OnAttack += 
            (direction) => ListenedComponentEventFired();
    }
}