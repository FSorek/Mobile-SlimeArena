using UnityEngine;

public class EntityAttackParticles : EventParticlePlayer<IEntityStateMachine>
{
    protected override void Subscribe()
    {
        ListenedComponent.OnEntityStateChanged += EntityStateChanged;
    }

    private void EntityStateChanged(IState state)
    {
        if (state is EntityAttack)
            ListenedComponentEventFired();
    }
}