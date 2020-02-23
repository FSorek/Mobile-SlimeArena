public class NPCOnTakeDamageParticles : EventParticlePlayer<EnemyNPC>
{
    protected override void Subscribe()
    {
        ListenedComponent.Health.OnTakeDamage += (damage) => ListenedComponentEventFired();
    }
}