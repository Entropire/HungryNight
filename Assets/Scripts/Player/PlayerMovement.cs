using StateMachine;
using UnityEngine;

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

    void Start()
    {
        if (!TryGetComponent(out Rb))
        {
            Debug.LogError($"Rigidbody2D not found on {gameObject.name}!");
        }
    }

    private void Update()
    {
        if (!Rb) return;

        IsGrounded();
        Movement();
        Jump();
    }

    private void Movement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        Rb.velocity = new Vector2(moveX * MaxSpeed, Rb.velocity.y);

        if (moveX is <= -0.1f or >= 0.1f)
            PlayerState.instance.IsWalking = true;
        else
            PlayerState.instance.IsWalking = false;
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            PlayerState.instance.IsJumping = true;
            initialY = transform.position.y; 
        }

        if (Input.GetKey(KeyCode.W) && PlayerState.instance.IsJumping)
        {
            if (transform.position.y - initialY >= MaxJumpHeight)
            {
                PlayerState.instance.IsJumping = false;
                return;
            }
            
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
        }
        
        if (Input.GetKeyUp(KeyCode.Space) || Rb.velocity.y <= 0)
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










