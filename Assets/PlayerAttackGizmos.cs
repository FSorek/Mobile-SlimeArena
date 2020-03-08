using System;
using UnityEngine;

public class PlayerAttackGizmos : MonoBehaviour
{
    [SerializeField] private AttackData attackData;
    [SerializeField] private bool draw;
    private Player player;
    private Vector2 attackSize;

    private void Awake()
    {
        player = GetComponent<Player>();
        attackSize = new Vector2(attackData.MinAttackRange, attackData.MaxAttackRange);
    }

    private void OnDrawGizmos()
    {
        if(!draw || player == null)
            return;
        Gizmos.DrawWireCube((Vector2)player.transform.position + player.AttackDirection, attackSize);
    }
}
