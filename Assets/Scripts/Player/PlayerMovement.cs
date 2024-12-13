using HungryNight.Player;
using StateMachine;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(PlayerState), typeof(PlayerInput)), 
    RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement : PlayerState
{
    private Rigidbody2D Rb;
    
    [SerializeField] private float MaxSpeed;
    
    [SerializeField] private float JumpForce = 2;
    [SerializeField] private Vector2 MaxJumpHeight = new(0, 3);


    void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Moving();
        Jumping();
    }

    private void Moving()
    {
        print(Instance);
        if (Instance.IsWalking)
        {
            Rb.velocity = new Vector2(Instance.LookingDirection.x * MaxSpeed, Rb.velocity.y);
        }
        else
        {
            Rb.velocity = new Vector2(0, Rb.velocity.y);
        }
    }

    private void Jumping()
    {
        MaxJumpHeight = (Vector2)Instance.LastGroundedLocation + MaxJumpHeight;
        if (Instance.IsJumping && Instance.LastGroundedLocation.y < MaxJumpHeight.y)
        {

        }
    }   
}   










