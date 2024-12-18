using HungryNight.Player;
using UnityEngine;

namespace StateMachine
{
    public class PlayerAnimator : MonoBehaviour
    {
        Animator animator;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            ResetTriggers();
            if (PlayerState.Instance.IsWalking)
            {
                animator.SetTrigger("IsWalking");
            }
            else if (PlayerState.Instance.IsJumping)
            {
                animator.SetTrigger("IsJumping");
            }
            else if (PlayerState.Instance.IsFalling)
            {
                animator.SetTrigger("IsFalling");
            }
            else if (PlayerState.Instance.IsAttacking)
            {
                animator.SetTrigger("IsAttacking");
            }
            else if (PlayerState.Instance.IsHit)
            {
                animator.SetTrigger("IsHit");
            }
            else
            {
                animator.SetTrigger("IsIdle");
            }
        }

        private void ResetTriggers()
        {
            animator.ResetTrigger("IsJumping");
            animator.ResetTrigger("IsFalling");
            animator.ResetTrigger("IsWalking");
            animator.ResetTrigger("IsAttacking");
            animator.ResetTrigger("IsHit");
            animator.ResetTrigger("IsIdle");
        }
    }
}