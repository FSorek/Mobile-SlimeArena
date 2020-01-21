using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public event Action<ITakeDamage> OnTargetHit = delegate { };
    public event Action<Vector2> OnAttack = delegate { };

    [SerializeField] private int damage;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private float attackDelay = .5f;
    [SerializeField] private Transform weapon;
    private RaycastHit2D[] targetsHit = new RaycastHit2D[20];
    private float lastAttackTime;
    private LayerMask enemyLayer;

    private Vector2 lastDirection;

    public Vector2 LastDirection => lastDirection;

    private void Awake()
    {
        enemyLayer = LayerMask.GetMask("NPC");
    }

    public void Attack(Vector2 direction)
    {
        lastDirection = direction;
        if(Time.time - lastAttackTime < attackDelay)
            return;
        var boxCastAngle = Vector2.SignedAngle(Vector2.up, direction);
        var resultAmount = Physics2D.BoxCastNonAlloc(weapon.position, attackSize, boxCastAngle, direction, targetsHit, 2f, enemyLayer);

        for (int i = 0; i < resultAmount; i++)
        {
            if(targetsHit[i].transform == transform)
                continue;
            var target = targetsHit[i].transform.GetComponent<ITakeDamage>();
            if (target != null)
            {
                target.TakeDamage(damage);
                OnTargetHit(target);
            }
        }

        OnAttack(direction);
        
        lastAttackTime = Time.time;
    }
    private void Update()
    {
        var boxCastAngle = Vector2.SignedAngle(Vector2.up, lastDirection);
        BoxCast(weapon.position, attackSize, boxCastAngle, lastDirection, 2f, enemyLayer);
    }
    private RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance,
        int mask)
    {
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, angle, direction, distance, mask);

//Setting up the points to draw the cast
        Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
        float w = size.x * 0.5f;
        float h = size.y * 0.5f;
        p1 = new Vector2(-w, h);
        p2 = new Vector2(w, h);
        p3 = new Vector2(w, -h);
        p4 = new Vector2(-w, -h);

        Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        p1 = q * p1;
        p2 = q * p2;
        p3 = q * p3;
        p4 = q * p4;

        p1 += origin;
        p2 += origin;
        p3 += origin;
        p4 += origin;

        Vector2 realDistance = direction.normalized * distance;
        p5 = p1 + realDistance;
        p6 = p2 + realDistance;
        p7 = p3 + realDistance;
        p8 = p4 + realDistance;


//Drawing the cast
        Color castColor = hit ? Color.red : Color.green;
        Debug.DrawLine(p1, p2, castColor);
        Debug.DrawLine(p2, p3, castColor);
        Debug.DrawLine(p3, p4, castColor);
        Debug.DrawLine(p4, p1, castColor);

        Debug.DrawLine(p5, p6, castColor);
        Debug.DrawLine(p6, p7, castColor);
        Debug.DrawLine(p7, p8, castColor);
        Debug.DrawLine(p8, p5, castColor);

        Debug.DrawLine(p1, p5, Color.grey);
        Debug.DrawLine(p2, p6, Color.grey);
        Debug.DrawLine(p3, p7, Color.grey);
        Debug.DrawLine(p4, p8, Color.grey);
        if (hit)
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal.normalized * 0.2f, Color.yellow);
        }

        return hit;
    }
}