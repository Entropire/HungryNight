using System;
using UnityEngine;

public class PlayerState : PlayerInput
{
    public static new PlayerState Instance;

    public new Vector2 LookingDirection { get; private set; } = new Vector2(1, 0);
    public new bool IsWalking;

    public Vector3 LastGroundedLocation;
    public bool IsJumping, IsFalling;
    public bool IsHit;
    public bool IsAttacking = false, AttackingOnCooldown = false;

    private Transform GroundCheckPos;
    private float GroundCheckRadius;
    float AttackCooldown, AttackDuration, Timer;


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        PlayerInput.Attacking += Attacked;
        PlayerInput.IsWalking += () => IsWalking = true;
        PlayerInput.LookingDirection += (Vector2 Vector2) => LookingDirection = Vector2;
    }

    void Attacked()
    {
        IsAttacking = true;
        AttackingOnCooldown = true;
    }

    public bool GetGroundState()
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
        }
        IsWalking = false;
        IsFalling = false;
        IsHit = false;
    }
}


// public void SetAttackCooldown(float NewCooldown)
// {
//     if (AttackCooldown < AttackDuration)
//     {
//         Debug.LogError("Please don't set the AttackWindow higher than the AttackCooldown - Nothing changed with this call");
//     }
//     else
//     {
//         AttackCooldown = NewCooldown;
//     }
// }
//
// public void SetAttackHitWindow(float HitWindow)
// {
//     if (AttackCooldown < AttackDuration)
//     {
//         Debug.LogError("Please don't set the AttackWindow higher than the AttackCooldown - Nothing changed with this call");
//     }
//     else
//     {
//         AttackDuration = HitWindow;
//     }
// }
//
// public void SetLookingDirection(Vector2 NewLookingDirection)
// {
//     if (LookingDirection == Vector2.up || LookingDirection == Vector2.down || LookingDirection == Vector2.left || LookingDirection == Vector2.right)
//     {
//         LookingDirection = NewLookingDirection;
//     }
//     else
//     {
//         Debug.LogError("Invalid looking direction in " + this);
//     }
// }