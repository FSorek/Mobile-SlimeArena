using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float healthUpSpawnChance;
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
        
        holder.AddPowerUp(powerUp);

        if(player.Health.CurrentHealth + 1 >= player.Health.MaxHealth)
            canSpawnPowerUp = false;
    }

    private void PlayerOnTakeDamage(int damage)
    {
        canSpawnPowerUp = true;
    }
}