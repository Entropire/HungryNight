using System;
using UnityEngine;

public class PlayerState : PlayerInput
{
    public static PlayerState Instance;

    [SerializeField] private Transform GroundCheckPos;
    [SerializeField] private float GroundCheckRadius;
    
    [NonSerialized] public Vector2 LookingDirection = Vector2.zero;
    [NonSerialized] public bool IsWalking;
    [NonSerialized] public Vector3 LastGroundedLocation;
    [NonSerialized] public bool IsJumping, IsFalling;
    [NonSerialized] public bool IsHit;
    [NonSerialized] public bool IsAttacking; 
    
    private bool AttackingOnCooldown;
    private float AttackCooldown, AttackDuration, Timer;

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        AttackingKeyPressed += Instance.Attacked;
        WalkingKeyPressed += (Bool) => Instance.IsWalking = Bool;
        JumpingKeyPressed += Instance.Jump;
        LookingDirectionUpdated += (Vector2) => Instance.LookingDirection = Vector2;
    }

    private void Attacked()
    {
        IsAttacking = true;
        AttackingOnCooldown = true;
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

    private void Update()
    {
        if (IsAttacking || AttackingOnCooldown)
        {
            Timer += Time.deltaTime;
            if (Timer > AttackDuration) IsAttacking = false;
            else if (Timer > AttackCooldown) AttackingOnCooldown = false; Timer = 0;
        }
    }
    
    

    private void FixedUpdate()
    {
        if (GetGroundState())
        {
           LastGroundedLocation = transform.position;
           if (IsFalling)
           {
               IsFalling = false;
           }
        }
        
        
        IsHit = false;
    }
}