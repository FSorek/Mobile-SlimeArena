public class NPCOnTakeDamageParticles : EventParticlePlayer<EnemyHealth>
{
    protected override void Subscribe()
    {
        ListenedComponent.OnTakeDamage += (damage) => ListenedComponentEventFired();
    }
}