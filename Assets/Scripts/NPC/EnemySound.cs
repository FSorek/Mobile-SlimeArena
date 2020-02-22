using UnityEngine;

public class EnemySound : EntitySound<EnemyNPC>
{
    [SerializeField] private AudioClip deathSound;
    protected override void Subscribe()
    {
        owner.Health.OnDeath += EnemyOnDeath;
    }
    private void EnemyOnDeath()
    {
        audioSource.PlayOneShot(deathSound);
    }
}