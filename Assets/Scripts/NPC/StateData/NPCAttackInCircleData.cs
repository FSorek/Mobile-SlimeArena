using UnityEngine;

[CreateAssetMenu(fileName = "NPC Attack In Circle Data")]
public class NPCAttackInCircleData : ScriptableObject
{
    [SerializeField] private int damage;
    [SerializeField] private int numberOfProjectiles;
    [Range(0, 180)]
    [SerializeField] private int nextAttackOFfset;
    [SerializeField] private float shootingRate;
    [SerializeField] private float minAttackRange = 5f;
    [SerializeField] private float maxAttackRange = 6f;

    public int Damage => damage;
    public int NumberOfProjectiles => numberOfProjectiles;
    public int NextAttackOFfset => nextAttackOFfset;
    public float ShootingRate => shootingRate;
    public float MinAttackRange => minAttackRange;
    public float MaxAttackRange => maxAttackRange;

}