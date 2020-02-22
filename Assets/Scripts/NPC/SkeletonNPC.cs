using UnityEngine;

public class SkeletonNPC : EnemyNPC
{
    [SerializeField] private int projectilesPerAttack;
    [SerializeField] private float delayBetweenSequenceAttacks;

    private int attackCounter;
}