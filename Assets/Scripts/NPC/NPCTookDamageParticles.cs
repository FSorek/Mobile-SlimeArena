public class NPCTookDamageParticles : EventParticlePlayer<NPCHealth>
{
    protected override void Subscribe()
    {
        listenedComponent.OnDeath += (npcHealth) => ListenedComponentEventFired();
    }
}