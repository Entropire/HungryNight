using UnityEngine;

[RequireComponent(typeof(PlayerState))]
public class PlayerAnimator : MonoBehaviour
{
  Animator animator;
  PlayerState playerState;
  PogoSlash pogoSlash;
  private bool IsDownSlash;

  private void Start()
  {
    animator = GetComponent<Animator>();
    playerState = GetComponent<PlayerState>();
    
  }

  private void Update()
  {
    IsDownSlash = GetComponent<Attacking>().OnBoxDown();
    ResetTriggers();
    if (playerState.IsWalking && !playerState.IsJumping)
    {
      animator.SetTrigger("IsWalking");
    }
    else if (playerState.IsJumping && !playerState.IsFalling && !IsDownSlash)
    {
      animator.SetTrigger("IsJumping");
    }
    else if (playerState.IsFalling && !playerState.IsJumping && !IsDownSlash)
    {
      animator.SetTrigger("IsFalling");
    }
    else if (playerState.IsAttacking && !IsDownSlash)
    {
      animator.Play("Attacking");
    }
    else if (playerState.IsHit)
    {
      animator.SetTrigger("IsHit");
    }
    else if (IsDownSlash)
    {
      animator.Play("HitDown");
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