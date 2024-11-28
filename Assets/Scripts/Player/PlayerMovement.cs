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

    void Start()
    {
        if (!TryGetComponent(out Rb))
        {
            Debug.LogError("Rigidbody2D not found on PlayerMovement!");
        }
    }

    private void Update()
    {
        if (!Rb) return;

        Movement();
        Jump();
    }

    private void Movement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        Rb.velocity = new Vector2(moveX * MaxSpeed, Rb.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.W) && IsGrounded())
        {
            PlayerState.IsJumping = true;
            initialY = transform.position.y; 
        }

        if (Input.GetKey(KeyCode.W) && PlayerState.IsJumping)
        {
            if (transform.position.y - initialY >= MaxJumpHeight)
            {
                PlayerState.IsJumping = false;
                return;
            }
            
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
        }
        
        if (Input.GetKeyUp(KeyCode.Space) || Rb.velocity.y <= 0)
        {
            PlayerState.IsJumping  = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, LayerMask.GetMask("Ground"));
    }
}   










