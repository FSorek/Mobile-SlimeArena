public class NPCOnDeathParticles : EventParticlePlayer<EnemyNPC>
{
    protected override void Subscribe()
    {
        ListenedComponent.Health.OnDeath += () => ListenedComponentEventFired();
    }
}