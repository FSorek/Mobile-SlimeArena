public class NPCOnTakeDamageParticles : EventParticlePlayer<EnemyNPC>
{
    protected override void Subscribe()
    {
        listenedComponent.OnTakeDamage += () => ListenedComponentEventFired();
    }
}