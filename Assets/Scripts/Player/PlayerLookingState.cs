using Unity.VisualScripting;
using UnityEngine;

namespace StateMachine
{
    public class PlayerState : MonoBehaviour
    {
        public static PlayerState instance;

        void Start()
        {
            if (instance == null)
                instance = this;
        }

        public Vector2 LookingDiraction = new Vector2(0, 0);
        public bool IsJumping, IsFalling;
        public bool IsHit;
        public bool IsAttacking = false, AttackingOnCooldown = false;
    }
}