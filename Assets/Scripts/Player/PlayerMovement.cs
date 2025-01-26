using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : PlayerState
{
    private Rigidbody2D Rb;

    [SerializeField] private float MaxSpeed;
    [SerializeField] private float JumpForce = 2;
    private bool JumpWasPressedThisFrame;
    public bool IsGrounded;
    private bool BeginJump;
    private RaycastHit2D GroundHit;
    private float JumpPercentage;


    void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Moving();
        Jumping();
        Rotate();
        if (IsGroundedState())
        {   
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }
    private bool IsGroundedState()
    {
        GroundHit = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2), new Vector2(0.1f, transform.localScale.x), 0f, Vector2.down, 0.25f, LayerMask.GetMask("Ground"));
        if (GroundHit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Moving()
    {
        if (IsWalking)
        {
            Rb.velocity = new Vector2(LookingDirection.x * MaxSpeed, Rb.velocity.y);
        }
        else
        {
            Rb.velocity = new Vector2(0, Rb.velocity.y);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) 
    {
        IsJumping = false;
        BeginJump = false;
    }

    private void Rotate()
    {
        if (LookingDirection == Vector2.left)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (LookingDirection == Vector2.right)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void Jumping()
    {
        JumpPercentage = (transform.position.y/MaxJumpHeightPos) * 100;
        if (IsJumping)
        {
            
            if (!JumpWasPressedThisFrame)
            {
                IsJumping = true;
                BeginJump = true;
                Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
                JumpWasPressedThisFrame = true;
            }
            
            if (JumpPercentage < 90 && BeginJump)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
            }
            else if (JumpPercentage >= 90 && JumpPercentage < 98 && BeginJump)
            {
                float reducedJumpForce = Mathf.Lerp(JumpForce, 0, (JumpPercentage - 90) / 10f);
                Rb.velocity = new Vector2(Rb.velocity.x, reducedJumpForce);
            }
            else if (JumpPercentage >= 98 && BeginJump)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, 0);
                IsJumping = false; 
                BeginJump = false; 
                IsFalling = true;  
            }      
            else
            {
                IsJumping = false;
            }
        }

        if (Rb.velocity.y < 0)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                BeginJump = false;
                JumpWasPressedThisFrame = false;
            }
            IsFalling = true;
        }
          
        if (Input.GetKeyUp(KeyCode.Space))
        {
            BeginJump = false;
            Rb.velocity = new Vector2(Rb.velocity.x, 1f);
            JumpWasPressedThisFrame = false;
        }

    }   
}   