using System.Collections;
using UnityEngine;

public class PlayerState : PlayerInput
{
    [SerializeField] private Transform GroundCheckPos;
    [SerializeField] private float GroundCheckRadius = .2f;
    [SerializeField] private float AttackCooldown = 0.3f;
    [SerializeField] private float AttackDuration = 2f;
    
    public Vector2 LookingDirection = Vector2.zero;
    public bool IsWalking;
    protected Vector3 LastGroundedLocation;
    public bool IsJumping, IsFalling;
    public bool IsHit;
    public bool IsAttacking; 
    protected float MaxJumpHeightPos;
    private bool AttackingOnCooldown;
    
    private float JumpHeight = 6f;

    
    private void Awake()
    {  
        AttackingKeyPressed += Attacked;
        WalkingKeyPressed += (Bool) => IsWalking = Bool;
        JumpingKeyPressed += Jump;
        LookingDirectionUpdated += (Vector2) => LookingDirection = Vector2;
    }

    

    private void Attacked()
    {
        if (!IsAttacking && !AttackingOnCooldown)
        {
            IsAttacking = true;
            AttackingOnCooldown = true;
            StartCoroutine(AttackCooldownTimer());
        }
    }

    private void Jump(bool keyPressed)
    {
        if (keyPressed)
        {
            if (GetGroundState())
            {
                IsJumping = true;
            }
        }
        else
        {
            IsJumping = false;
        }
    }

    private bool GetGroundState()
    {
        return Physics2D.OverlapCircle(GroundCheckPos.position, GroundCheckRadius, LayerMask.GetMask("Ground"));
    }
    
    private IEnumerator AttackCooldownTimer()
    {
        yield return new WaitForSeconds(AttackDuration);
        IsAttacking = false;
        yield return new WaitForSeconds(AttackCooldown);
        AttackingOnCooldown = false;
    }
    

    private void FixedUpdate()
    {
        if (GetGroundState())
        {
           LastGroundedLocation = transform.position;
           MaxJumpHeightPos = LastGroundedLocation.y + JumpHeight;
           if (IsFalling)
           {
                IsFalling = false;
           }
        } 
        IsHit = false;
    }
}