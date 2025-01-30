using UnityEngine;

namespace StateMachine
{
    [RequireComponent(typeof(PlayerState))]
    public class PlayerAnimator : MonoBehaviour
    {
        Animator animator;
        PlayerState playerState;

        private void Start()
        {
            animator = GetComponent<Animator>();
            playerState = GetComponent<PlayerState>();
        }

        private void Update()
        {
            ResetTriggers();
            if (playerState.IsWalking && !playerState.IsJumping)
            {
                animator.SetTrigger("IsWalking");
            }
            else if (playerState.IsJumping && !playerState.IsFalling)
            {
                animator.SetTrigger("IsJumping");
            }
            else if (playerState.IsFalling  && !playerState.IsJumping)
            {
                animator.SetTrigger("IsFalling");
            }
            else if (playerState.IsAttacking)
            {
                animator.SetTrigger("IsAttacking");
            }
            else if (playerState.IsHit)
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