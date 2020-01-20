using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private PlayerInput playerInput;
    private IMovement initialMovement;
    private IMovement currentMovement;
    private bool isMoving;

    public PlayerInput PlayerInput => playerInput;
    public bool IsMoving => isMoving;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void Start()
    {
        currentMovement = initialMovement = new InputMovement(playerInput, transform);
    }

    private void Update()
    {
        playerInput.Tick();
    }

    private void FixedUpdate()
    {
        isMoving = currentMovement.Tick(moveSpeed);
    }

    public void ChangeMovementStyle(IMovement movement)
    {
        currentMovement = movement;
        currentMovement.Initialize();
    }

    public void ResetMovementStyle()
    {
        ChangeMovementStyle(initialMovement);
    }
}

//to-do:
        
//make touch events fire for 2+ touches :)
//abstract some of this logic and create a static attack joystick :)
//keep attack logic the same, allow all directions of attack :)

//turn the sword into a dagger so that stabbing makes more sense :)

//make ability have animation :)
//make the ability have a time pool :)
//refill time pool with kills outside of ability :)

//create the arena :)
//adjust npc logic for collisions :)
//adjust layers and collisions :)

//enemy keeps moving if direct raycast hits a wall
//stop velocity after using ability

//create enemy spawners 
//add enemy deaths and pooling

//add kill count
//add a simple menu
//add player deaths