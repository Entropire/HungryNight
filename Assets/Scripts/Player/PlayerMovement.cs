using UnityEngine;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : PlayerState
{
    private Rigidbody2D Rb;
    
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float JumpForce = 2;
    [SerializeField] private float JumpTime = 0.5f;
    private bool JumpWasPressedThisFrame;
    private float jumpTimeCounter;
    public bool IsGrounded;
    private RaycastHit2D GroundHit;
    private Collider2D coll;
    // c[SerializeField] private float MaxJumpHeight = 8f;
    // private float MaxJumpHeightPos;


    void Start()
    {
        coll ??= GetComponent<Collider2D>();
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
    }
    private bool IsGroundedState()
    {
        GroundHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.25f, LayerMask.GetMask("Ground"));
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
        if (Instance.IsWalking)
        {
            Rb.velocity = new Vector2(Instance.LookingDirection.x * MaxSpeed, Rb.velocity.y);
        }
        else
        {
            Rb.velocity = new Vector2(0, Rb.velocity.y);
        }
    }

    private void Rotate()
    {
        if (Instance.LookingDirection == Vector2.left)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (Instance.LookingDirection == Vector2.right)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void Jumping()
    {
        
        
        if (Instance.IsJumping)
        {
            if (!JumpWasPressedThisFrame)
            {
                IsJumping = true;
                jumpTimeCounter = JumpTime;
                Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
                JumpWasPressedThisFrame = true;
            }
            
            if (jumpTimeCounter > 0 && IsJumping)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else if (jumpTimeCounter == 0)
            {
                IsJumping = false;
                IsFalling = true;
            }
            else
            {
                IsJumping = false;
            }
        }
        else
        {
            IsJumping = false;
        }

        if (Rb.velocity.y < 0)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                JumpWasPressedThisFrame = false;
            }
            Instance.IsFalling = true;
        }
        if (IsGrounded)
        {       
            if (Input.GetKeyUp(KeyCode.Space))
            {
                JumpWasPressedThisFrame = false;
            }
        }
    }   
}   