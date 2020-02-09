using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbilityData")]
public class PlayerAbilityData : ScriptableObject
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private float damageRadius;
    [SerializeField] private int damagePerTick;
    [SerializeField] private float tickRate = .5f;
    [SerializeField] private int maxPoolAmount = 3;
    [SerializeField] private float refillAmount = 1f;
    [SerializeField] private float tornadoAcceleration = 2f;
    [SerializeField] private float tornadoMaxSpeed = 10f;

    public GameObject WallPrefab => wallPrefab;
    public float DamageRadius => damageRadius;
    public int DamagePerTick => damagePerTick;
    public float TickRate => tickRate;
    public int MaxPoolAmount => maxPoolAmount;

    public float TornadoAcceleration => tornadoAcceleration;

    public float RefillAmount => refillAmount;

    public float TornadoMaxSpeed => tornadoMaxSpeed;
}