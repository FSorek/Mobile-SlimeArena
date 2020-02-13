using UnityEngine;

[CreateAssetMenu(fileName = "NPC Attack Data")]
public class NPCAttackData : ScriptableObject
{
    [SerializeField] private Vector2 shootPositionOffset;
    public int Damage;
    public float ShootingRate;
    public float MinAttackRange = 5f;
    public float MaxAttackRange = 8f;
    public float AccuracyOffset;
    public Vector2 ShootPositionOffset => shootPositionOffset;
}