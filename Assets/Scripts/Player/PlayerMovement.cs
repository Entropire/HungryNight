using HungryNight.Player;
using StateMachine;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(PlayerState)), RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D Rb;
    private PlayerState playerState;
    private float initialY;
    
    [SerializeField] private float MaxSpeed;
    
    [SerializeField] private float JumpForce;
    [SerializeField] private float MaxJumpHeight;
    
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float GroundCheckRadius;

    private bool isGrounded;
    private Vector3 LastGroundedLocation;
    private Vector2 PlayerInput;


    void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody2D>();
        playerState = gameObject.GetComponent<PlayerState>();
    }

    private void Update()
    {
        IsGrounded();
        Movement();
        Jump();
    }

    private void Movement()
    {
        if (playerState.IsWalking)
        {
            Rb.velocity = new Vector2(playerState.LookingDirection.x * MaxSpeed, Rb.velocity.y);
        }
        else
        {
            Rb.velocity = new Vector2(0, Rb.velocity.y);
        }
    }

    private void Jump()
    {
        float Vertical = Input.GetAxis("Vertical");

        if (Vertical > 0 && isGrounded)
        {
            playerState.IsJumping = true;
            initialY = transform.position.y; 
        }

        if (Vertical > 0 && playerState.IsJumping)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
            if (transform.position.y - initialY >= MaxJumpHeight)
            {
                playerState.IsJumping = false;
                playerState.IsFalling = true;
                return;
            }
        }
        
        if (Vertical <= 0)
        {
            playerState.IsJumping  = false;
            playerState.IsFalling = true;
        }

        
        if (Rb.velocity.y == 0)
        {
            playerState.IsFalling = false;
        }
    }   

    private void IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, LayerMask.GetMask("Ground"));

        if (isGrounded)
        {
            LastGroundedLocation = transform.position;
        }
    }
}   










