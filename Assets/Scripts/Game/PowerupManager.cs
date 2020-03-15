using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float healthUpSpawnChance;
    [SerializeField] private ObjectPool powerUpEffectPool;
    private IPowerUp powerUp;
    private ITakeDamage playerHealth;
    private bool canSpawnPowerUp;

    private void Awake()
    {
        playerHealth = FindObjectOfType<Player>().GetComponent<ITakeDamage>();
        NpcSpawnerSystem.Instance.OnSpawned += NpcSpawned;
        powerUp = new HealthUp();
    }

    private void Start()
    {
        playerHealth.OnTakeDamage += PlayerOnTakeDamage;
        var particleEffect = powerUpEffectPool.Get().GetComponent<ParticleSystem>();
        powerUp.ParticleSystem = particleEffect;
    }

    private void NpcSpawned(EnemyNPC npc)
    {
        var holder = npc.GetComponent<IPowerUpHolder>();
        
        if(holder == null 
           || !canSpawnPowerUp 
           || Random.value > healthUpSpawnChance)
            return;
        
        holder.AddPowerUp(powerUp);
        if(playerHealth.CurrentHealth + 1 >= playerHealth.MaxHealth)
            canSpawnPowerUp = false;
    }

    private void PlayerOnTakeDamage(int damage)
    {
        canSpawnPowerUp = true;
    }
}

public interface IPowerUpHolder
{
    void AddPowerUp(IPowerUp power);
}