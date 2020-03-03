using UnityEngine;

[CreateAssetMenu(fileName = "Attack Data")]
public class AttackData : ScriptableObject
{
    [SerializeField] private int damage;
    [SerializeField] private float shootingRate;
    [SerializeField] private float minAttackRange;
    [SerializeField] private float maxAttackRange;
    [SerializeField] private float castingTime;

    public int Damage => damage;
    public float ShootingRate => shootingRate;
    public float MinAttackRange => minAttackRange;
    public float MaxAttackRange => maxAttackRange;
    public float CastingTime => castingTime;
}