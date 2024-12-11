using StateMachine;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(PlayerState)), RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D Rb;
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
    }

    private void Update()
    {
        IsGrounded();
        Movement();
        Jump();
    }

    private void Movement()
    {

        float horizontal = Input.GetAxis("Horizontal");
        Rb.velocity = new Vector2(horizontal * MaxSpeed, Rb.velocity.y);

        if (horizontal is <= -0.1f or >= 0.1f)
        {
            PlayerState.instance.IsWalking = true;
        }
        else
        {
            PlayerState.instance.IsWalking = false;
        }
    }

    private void Jump()
    {
        float Vertical = Input.GetAxis("Vertical");

        if (Vertical > 0 && isGrounded)
        {
            PlayerState.instance.IsJumping = true;
            initialY = transform.position.y; 
        }

        if (Vertical > 0 && PlayerState.instance.IsJumping)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
            if (transform.position.y - initialY >= MaxJumpHeight)
            {
                PlayerState.instance.IsJumping = false;
                PlayerState.instance.IsFalling = true;
                return;
            }
        }
        
        if (Vertical <= 0)
        {
            PlayerState.instance.IsJumping  = false;
            PlayerState.instance.IsFalling = true;
        }

        
        if (Rb.velocity.y == 0)
        {
            PlayerState.instance.IsFalling = false;
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










