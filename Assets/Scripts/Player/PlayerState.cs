using Unity.VisualScripting;
using UnityEngine;

namespace StateMachine
{
    public class PlayerState : MonoBehaviour
    {
        #region FieldAndPropertyHandling

        [SerializeField] float AttackCooldown, AttackDuration, Timer;
        public static PlayerState instance;

        public void SetAttackCooldown(float NewCooldown)
        {
            if (AttackCooldown < AttackDuration)
            {
                Debug.LogError("Please don't set the AttackWindow higher than the AttackCooldown - Nothing changed with this call");
            }
            else
            {
                AttackCooldown = NewCooldown;
            }
        }

        public void SetAttackHitWindow(float HitWindow)
        {
            if (AttackCooldown < AttackDuration)
            {
                Debug.LogError("Please don't set the AttackWindow higher than the AttackCooldown - Nothing changed with this call");
            }
            else
            {
                AttackDuration = HitWindow;
            }
        }

        public void SetLookingDirection(Vector2 NewLookingDirection)
        {
            if (LookingDirection == Vector2.up || LookingDirection == Vector2.down || LookingDirection == Vector2.left || LookingDirection == Vector2.right)
            {
                LookingDirection = NewLookingDirection;
            } else
            {
                Debug.LogError("Invalid looking direction in " + this);
            }
        }
        void Start()
        {
            if (instance == null)
                instance = this;

            InputMaganger.Mepping += Attacked;
        }
        #endregion

        public Vector2 LookingDirection { get; private set; } = new Vector2(1, 0);
        public bool IsJumping, IsFalling, IsWalking;
        public bool IsHit;
        public bool IsAttacking = false, AttackingOnCooldown = false;

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