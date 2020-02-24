using UnityEngine;

[CreateAssetMenu(fileName = "NPC Attack Data")]
public class NPCAttackData : ScriptableObject
{
    [SerializeField] private int damage;
    [SerializeField] private float shootingRate;
    [SerializeField] private float minAttackRange = 5f;
    [SerializeField] private float maxAttackRange = 6f;
    [Range(0, .9f)]
    [SerializeField] private float accuracySpread;

    public int Damage => damage;
    public float ShootingRate => shootingRate;
    public float MinAttackRange => minAttackRange;
    public float MaxAttackRange => maxAttackRange;
    public float AccuracySpread => accuracySpread;
}