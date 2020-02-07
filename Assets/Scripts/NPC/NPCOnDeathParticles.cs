public class NPCOnDeathParticles : EventParticlePlayer<EnemyNPC>
{
    protected override void Subscribe()
    {
        listenedComponent.OnDeath += () => ListenedComponentEventFired();
    }
}