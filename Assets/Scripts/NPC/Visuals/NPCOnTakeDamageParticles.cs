public class NPCOnTakeDamageParticles : EventParticlePlayer<EnemyNPC>
{
    protected override void Subscribe()
    {
        listenedComponent.Health.OnTakeDamage += (damage) => ListenedComponentEventFired();
    }
}