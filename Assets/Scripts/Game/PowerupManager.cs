using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float healthUpSpawnChance;
    [SerializeField] private ObjectPool powerUpEffectPool;
    private ITakeDamage playerHealth;
    private int healthToSpawn = 0;

    private void Awake()
    {
        playerHealth = FindObjectOfType<Player>().GetComponent<ITakeDamage>();
        NpcSpawnerSystem.Instance.OnSpawned += NpcSpawned;
    }

    private void Start()
    {
        playerHealth.OnTakeDamage += PlayerOnTakeDamage;
    }

    private void NpcSpawned(EnemyNPC npc)
    {
        var holder = npc.GetComponent<IPowerUpHolder>();
        
        if(holder == null 
           || healthToSpawn <= 0 
           || Random.value > healthUpSpawnChance)
            return;
        
        var powerUp = new HealthUp();
        powerUp.Effect = powerUpEffectPool.Get();
        holder.AddPowerUp(powerUp);

        healthToSpawn--;
    }

    private void PlayerOnTakeDamage(int damage)
    {
        healthToSpawn++;
    }
}