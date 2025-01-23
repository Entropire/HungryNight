﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.XR;
using UnityEngine;

public class PlayerState : PlayerInput
{
    public static PlayerState Instance;

    [SerializeField] private Transform GroundCheckPos;
    [SerializeField] private float GroundCheckRadius = .2f;
    [SerializeField] private float AttackCooldown = 0.3f;
    [SerializeField] private float AttackDuration = 2f;
    
    [NonSerialized] public Vector2 LookingDirection = Vector2.zero;
    [NonSerialized] public bool IsWalking;
    [NonSerialized] public Vector3 LastGroundedLocation;
    [NonSerialized] public bool IsJumping, IsFalling;
    [NonSerialized] public bool IsHit;
    [NonSerialized] public bool IsAttacking; 
    // [NonSerialized] public bool IsGrounded;
    private RaycastHit2D GroundHit;
    private Collider2D coll;
    private bool AttackingOnCooldown;

    
    private void Awake()
    {
        // KeyReleased = true; 
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
                // IsGrounded = false;
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