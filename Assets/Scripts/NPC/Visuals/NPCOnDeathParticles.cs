public class NPCOnDeathParticles : EventParticlePlayer<EnemyHealth>
{
    protected override void Subscribe()
    {
        ListenedComponent.OnTakeDamage += ListenedComponentOnTakeDamage;
    }

    private void ListenedComponentOnTakeDamage(int amount)
    {
        if(ListenedComponent.CurrentHealth <= 0)
            ListenedComponentEventFired();
    }
}