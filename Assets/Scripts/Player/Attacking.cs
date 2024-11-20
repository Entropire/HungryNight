using System;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    public static Action<bool> IsAttacking, AttackingOnCooldown;
    public static float AttackCooldown { get; private set; }
    public static float AttackHitWindow { get; private set; }

    private float AttackingCooldownTime = 0;
    private float AttackingHitWindowTime = 0;

    private void Start()
    {
        AttackingOnCooldown += ResetTimer;
        AttackingOnCooldown += HitCooldown;
    }



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
        if (AttackingCooldownTime < AttackCooldown) Timers();

        if (Input.GetMouseButtonDown(0))
        {
            Checks();
        }
    }

    void Checks()
    {
        AttackingOnCooldown?.Invoke(AttackingCooldownTime >= AttackCooldown);
    }

    private void Timers()
    {
        AttackingHitWindowTime += Time.deltaTime;
        AttackingCooldownTime += Time.deltaTime;
    }

    void HitCooldown(bool CanAttack)
    {
        if (CanAttack)
        {
            IsAttacking?.Invoke(true);
        }
        else
        {
            IsAttacking?.Invoke(false);
        }
        
    }

    private void ResetTimer(bool Reset)
    {
        if (Reset)
        {
            AttackingCooldownTime = 0;
            AttackingHitWindowTime = 0;
        }
    }
}
