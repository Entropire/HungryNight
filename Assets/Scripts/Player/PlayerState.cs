using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : PlayerInput
{
    public static PlayerState Instance;

    [SerializeField] private Transform GroundCheckPos;
    [SerializeField] private float GroundCheckRadius = .2f;
    [SerializeField] private float AttackCooldown = 0.5f;
    [SerializeField] private float AttackDuration = 0.5f;
    
    [NonSerialized] public Vector2 LookingDirection = Vector2.zero;
    [NonSerialized] public bool IsWalking;
    [NonSerialized] public Vector3 LastGroundedLocation;
    [NonSerialized] public bool IsJumping, IsFalling;
    [NonSerialized] public bool IsHit;
    [NonSerialized] public bool IsAttacking; 
    
    private bool AttackingOnCooldown;

    
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
           if (IsFalling)
           {
               IsFalling = false;
           }
        }
        
        
        IsHit = false;
    }
}