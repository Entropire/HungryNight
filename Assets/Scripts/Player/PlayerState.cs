using System;
using UnityEngine;

namespace HungryNight.Player
{
    public class PlayerState : MonoBehaviour   
    {
        [SerializeField] float AttackCooldown, AttackDuration, Timer;
        public Vector2 LookingDirection { get; private set; } = new Vector2(1, 0);
        public bool IsJumping, IsFalling, IsWalking;
        public bool IsHit;
        public bool IsAttacking = false, AttackingOnCooldown = false;

        private void Start()
        {
            if (PlayerInput.instance != null)
            {
                PlayerInput.instance.LookingDirection += (vec) => LookingDirection = vec;
                PlayerInput.instance.IsWalking += (walking) => IsWalking = walking;
                PlayerInput.instance.IsJumping += (jumping) => IsJumping = jumping;
                PlayerInput.instance.IsAttack += () => Attacked();
            }
        }

        void Attacked()
        {
            IsAttacking = true;
            AttackingOnCooldown = true;
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