using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event Action<Vector2> LookingDirection;
    public static event Action IsWalking;
    public static event Action Attack;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack?.Invoke();
        }
        switch (Input.inputString.ToLower())
        {
            case "w":
                LookingDirection?.Invoke(Vector2.up);
                break;
            case "a":
                LookingDirection?.Invoke(Vector2.left);
                IsWalking?.Invoke();
                break;
            case "d":
                LookingDirection?.Invoke(Vector2.right);
                IsWalking?.Invoke();
                break;
            case "s":
                LookingDirection?.Invoke(Vector2.down);
                break;
            default:
                break;
        }
    }
}
