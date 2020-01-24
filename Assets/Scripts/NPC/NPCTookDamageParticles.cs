public class NPCTookDamageParticles : EventParticlePlayer<EnemyNPC>
{
    protected override void Subscribe()
    {
        listenedComponent.OnDeath += () => ListenedComponentEventFired();
    }
}