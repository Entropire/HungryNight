using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event Action<Vector2> LookingDirectionUpdated;
    public static event Action<bool> WalkingKeyPressed;
    public static event Action<bool> JumpingKeyPressed;
    public static event Action AttackingKeyPressed;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AttackingKeyPressed?.Invoke();
        }

        if (Input.GetKey(KeyCode.W))
        {
            LookingDirectionUpdated?.Invoke(Vector2.up);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            LookingDirectionUpdated?.Invoke(Vector2.down);
        }

        if (Input.GetKey(KeyCode.D))
        {
            LookingDirectionUpdated?.Invoke(Vector2.right);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            LookingDirectionUpdated?.Invoke(Vector2.left);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            JumpingKeyPressed?.Invoke(true);
        }
        else
        {
            JumpingKeyPressed?.Invoke(false);
        }
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            WalkingKeyPressed?.Invoke(true);
        }
        else
        {
            WalkingKeyPressed?.Invoke(false);
        }
    }
}
