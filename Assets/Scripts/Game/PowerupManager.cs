using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float healthUpSpawnChance;
    [SerializeField] private ObjectPool powerUpEffectPool;
    private IPowerUp powerUp;
    private Player player;
    private bool canSpawnPowerUp;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        NpcSpawnerSystem.Instance.OnSpawned += NpcSpawned;
        powerUp = new HealthUp();
    }

    private void Start()
    {
        player.Health.OnTakeDamage += PlayerOnTakeDamage;
    }

    private void NpcSpawned(EnemyNPC npc)
    {
        var holder = npc.GetComponent<IPowerUpHolder>();
        
        if(holder == null 
           || !canSpawnPowerUp 
           || Random.value > healthUpSpawnChance)
            return;

        var particleEffect = powerUpEffectPool.Get().GetComponent<ParticleSystem>();
        holder.AddPowerUp(powerUp, particleEffect);

        if(player.Health.CurrentHealth + 1 >= player.Health.MaxHealth)
            canSpawnPowerUp = false;
    }

    private void PlayerOnTakeDamage(int damage)
    {
        canSpawnPowerUp = true;
    }
}