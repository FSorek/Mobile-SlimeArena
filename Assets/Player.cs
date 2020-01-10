using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float registerMoveDistance = 100f;
    private readonly PlayerInput playerInput = new PlayerInput();
    private bool isMoving;

    public PlayerInput PlayerInput => playerInput;
    public bool IsMoving => isMoving;
    
    private void Update()
    {
        playerInput.Tick();
        if (playerInput.MoveVector.magnitude > registerMoveDistance)
        {
            transform.Translate(Time.deltaTime * moveSpeed * playerInput.MoveVector.normalized);
            if(!isMoving) 
                isMoving = true;
        }
        else if (isMoving)
        {
            isMoving = false;
        }
    }
}