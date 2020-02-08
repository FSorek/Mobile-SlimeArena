using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttackData")]
public class PlayerAttackData : ScriptableObject
{
    [SerializeField] private int damage = 1;
    [SerializeField] private Vector2 attackSize = new Vector2(1, .5f);
    [SerializeField] private float attackDelay = .5f;
    [SerializeField] private float attackRootDuration;

    public int Damage => damage;
    public Vector2 AttackSize => attackSize;
    public float AttackDelay => attackDelay;

    public float AttackRootDuration => attackRootDuration;
}