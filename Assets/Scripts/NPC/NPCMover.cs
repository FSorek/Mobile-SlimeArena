using System;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Rigidbody2D))]
public class NPCMover : MonoBehaviour
{
    private const float WaypointStoppingDistance = 1f;
    [SerializeField] private float moveSpeed;

    private int currentWaypoint;
    private Seeker seeker;
    private Rigidbody2D rigidBody;
    private Path path;
    public bool IsMoving { get; private set; }
    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        rigidBody = GetComponent<Rigidbody2D>();
        IsMoving = false;
    }

    private void OnDisable()
    {
        IsMoving = false;
    }

    private void Update()
    {
        if(!IsMoving || !path.IsDone())
            return;
        var moveDirection = ((Vector2)path.vectorPath[currentWaypoint] - rigidBody.position).normalized;
        rigidBody.MovePosition(rigidBody.position + Time.fixedDeltaTime * moveSpeed * moveDirection);
        if (Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]) < WaypointStoppingDistance)
            currentWaypoint++;
        if (currentWaypoint >= path.vectorPath.Count - 1)
            IsMoving = false;
    }
    public void MoveTo(Vector2 destination)
    {
        path = seeker.StartPath(transform.position, destination);
        currentWaypoint = 0;
        IsMoving = true;
    }

    public void Stop()
    {
        IsMoving = false;
    }
}