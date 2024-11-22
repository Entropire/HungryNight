using System;
using UnityEngine;

public class HitTimers : MonoBehaviour
{
    public static Action<bool> IsAttacking, AttackingOnCooldown;
    public static float AttackCooldown { get; private set; }
    public static float AttackHitWindow { get; private set; }

    private float AttackingCooldownTime = 0;
    private float AttackingHitWindowTime = 0;

    public static void SetAttackCooldown(float NewCooldown)
    {
        if (AttackCooldown < AttackHitWindow)
        {
            Debug.LogError("Please don't set the AttackWindow higher than the AttackCooldown - Nothing changed with this call");
        }
        else
        {
            AttackCooldown = NewCooldown;
        }
    }

    public static void SetAttackHitWindow(float HitWindow)
    {
        if (AttackCooldown < AttackHitWindow)
        {
            Debug.LogError("Please don't set the AttackWindow higher than the AttackCooldown - Nothing changed with this call");
        }
        else
        {
            AttackHitWindow = HitWindow;
        }
    }

    void Update()
    {
        if (StateMachine.PlayerState.AttackingOnCooldown) Timers(); Checks();
        if (!StateMachine.PlayerState.AttackingOnCooldown && Input.GetMouseButtonDown(0))
        {
            Count();
        }
    }

    void Checks()
    {
        AttackingOnCooldown?.Invoke(AttackingCooldownTime <= AttackCooldown);
        AttackingHitWindowCheck();
    }

    private void Timers()
    {
        AttackingHitWindowTime += Time.deltaTime;
        AttackingCooldownTime += Time.deltaTime;
    }

    void AttackingHitWindowCheck()
    {
        if (AttackingHitWindowTime >= AttackHitWindow)
        {
            IsAttacking?.Invoke(true);
        }
        else
        {
            IsAttacking?.Invoke(false);
        }
    }

    private void Count()
    {
        AttackingCooldownTime = 0;
        AttackingHitWindowTime = 0;
    }
}
