namespace StateMachine
{
    public static class PlayerState
    {
        public static bool Up, Down, Left, Right;
        public static bool IsJumping, IsFalling;
        public static bool TookDamage;
        public static bool IsAttacking = false, AttackingOnCooldown = false;

        static PlayerState()
        {
            HitTimers.IsAttacking += AttackingCheck;
            HitTimers.AttackingOnCooldown += AttackingCooldownCheck;
        }

        private static void AttackingCheck(bool Attacking)
        {
            if (Attacking)
            {
                IsAttacking = true;
            } else
            {
                IsAttacking = false;
            }
        }

        private static void AttackingCooldownCheck(bool OnCooldown)
        {
            if (!OnCooldown)
            {
                AttackingOnCooldown = false;
            }
            else
            {
                AttackingOnCooldown = true;
            }
        }
    }
}