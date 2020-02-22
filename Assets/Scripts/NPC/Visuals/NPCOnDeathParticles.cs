public class NPCOnDeathParticles : EventParticlePlayer<EnemyNPC>
{
    protected override void Subscribe()
    {
        listenedComponent.Health.OnDeath += () => ListenedComponentEventFired();
    }
}