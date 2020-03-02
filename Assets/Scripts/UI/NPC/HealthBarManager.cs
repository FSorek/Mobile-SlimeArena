using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    private readonly Dictionary<EnemyNPC, HealthbarUI> healthBars = new Dictionary<EnemyNPC, HealthbarUI>();
    [SerializeField] private ObjectPool HealthbarPool;

    private void Awake()
    {
        EnemyNPC.OnSpawned += AddHealthBar;
        EnemyNPC.OnDespawned += RemoveHealthBar;
    }

    private void RemoveHealthBar(EnemyNPC obj)
    {
        if (!healthBars.ContainsKey(obj) || healthBars[obj] == null) return;
        healthBars[obj].gameObject.ReturnToPool();
        healthBars.Remove(obj);
    }

    private void AddHealthBar(EnemyNPC npc)
    {
        if(npc.Health.MaxHealth <= 1)
            return;
        if (!healthBars.ContainsKey(npc))
        {
            var hpBar = HealthbarPool.Get().GetComponent<HealthbarUI>();
            healthBars.Add(npc, hpBar);
            hpBar.SetHealth(npc);
            hpBar.gameObject.SetActive(true);
        }
    }
}