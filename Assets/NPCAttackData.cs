using UnityEngine;

[CreateAssetMenu(fileName = "NPC Move Data")]
public class NPCAttackData : ScriptableObject
{
    public int Damage;
    public float ShootingRate;
    public float MinAttackRange = 5f;
    public float MaxAttackRange = 8f;
    public GameObject ProjectileModel;
}