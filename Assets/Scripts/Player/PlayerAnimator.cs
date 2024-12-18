using UnityEngine;

namespace StateMachine
{
    public class PlayerAnimator : MonoBehaviour
    {
        Animator animator;
        private void Start()
        {
            if (!!TryGetComponent(out animator))
            {
                Debug.LogError($"animator not found on {gameObject.name}!");
            }
        }

        private void Update()
        {
            ResetTriggers();

            if (PlayerState.instance.IsJumping)
            {
                animator.SetTrigger("IsJumping");
            }
            if (PlayerState.instance.IsFalling)
            {
                animator.SetTrigger("IsFalling");
            }
            if (PlayerState.instance.IsHit)
            {
                animator.SetTrigger("IsHit");
            }
        }

        private void ResetTriggers()
        {
            animator.ResetTrigger("IsJumping");
            animator.ResetTrigger("IsFalling");
            animator.ResetTrigger("IsWalking");
            animator.ResetTrigger("IsHit");
        }
    }
}