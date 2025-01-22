using UnityEngine;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : PlayerState
{
    private Rigidbody2D Rb;
    
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float JumpForce = 2;
    [SerializeField] private float MaxJumpHeight = 3f;
    private float MaxJumpHeightPos;


    void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Moving();
        Jumping();
        Rotate();
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
        MaxJumpHeightPos = Instance.LastGroundedLocation.y + MaxJumpHeight;
        
        if (Instance.IsJumping && transform.position.y < MaxJumpHeightPos)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce * Mathf.Clamp01(1 - transform.position.y / MaxJumpHeight));
            
            float forceMultiplier =  Mathf.Clamp01(1 - transform.position.y / MaxJumpHeight);
            
            if (forceMultiplier > 0)
            {
                Vector3 upwardForce = Vector3.up * (JumpForce * forceMultiplier);
                Rb.AddForce(upwardForce);
            }
        }
        else
        {
            IsJumping = false;
        }

        if (Rb.velocity.y < 0)
        {
            Instance.IsFalling = true;
        }
    }   
}   