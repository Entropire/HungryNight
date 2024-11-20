using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace StateMachine
{
    public static class PlayerState
    {
       static bool Up, Down, Left, Right;
        static bool IsJumping, IsFalling;
        static bool TookDamage;
        static bool IsAttacking;
    }
}