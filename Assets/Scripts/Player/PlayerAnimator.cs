using UnityEngine;

namespace StateMachine
{
    [RequireComponent(typeof(Animator))]
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
            if (PlayerState.instance.IsWalking)
            {
                animator.SetTrigger("IsWalking");
            }
            else if (PlayerState.instance.IsJumping)
            {
                animator.SetTrigger("IsJumping");
            }
            else if (PlayerState.instance.IsFalling)
            {
                animator.SetTrigger("IsFalling");
            }
            else if (PlayerState.instance.IsAttacking)
            {
                animator.SetTrigger("IsAttacking");
            }
            else if (PlayerState.instance.IsHit)
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