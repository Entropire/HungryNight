using StateMachine;
using UnityEngine;

#region RequireComponents
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(InputMaganger))]
#endregion
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

        InputMaganger.MoveDirection += (diraction) =>
        {
            PlayerInput = diraction;
        };
    }

    private void Update()
    {
        IsGrounded();
        Movement();
        Jump();
    }

    private void Movement()
    {
        Rb.velocity = new Vector2(PlayerInput.x * MaxSpeed, Rb.velocity.y);

        if (PlayerInput.x is <= -0.1f or >= 0.1f)
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
        if (PlayerInput.y > 0)
        {

        }

        if (PlayerInput.y > 0 && isGrounded)
        {
            PlayerState.instance.IsJumping = true;
            initialY = transform.position.y; 
        }

        if (PlayerInput.y > 0 && PlayerState.instance.IsJumping)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
            if (transform.position.y - initialY >= MaxJumpHeight)
            {
                PlayerState.instance.IsJumping = false;
                return;
            }
        }
        
        if (Rb.velocity.y <= 0)
        {
            PlayerState.instance.IsJumping  = false;
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










