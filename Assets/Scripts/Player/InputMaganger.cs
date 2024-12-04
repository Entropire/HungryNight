using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMaganger : MonoBehaviour
{
    public static event Action<Vector2> MoveDirection;
    public static event Action<Vector2> LookingDirection;
    public static event Action Mepping;
    public static event Action Jumping;
    private InputAction Move;
    private InputAction Look;
    private InputAction Attack;
    private InputAction Jump;

    void Start()
    {
        Move = InputSystem.actions.FindAction("Move");
        Look = InputSystem.actions.FindAction("Look");
        Attack = InputSystem.actions.FindAction("Attack");
        Jump = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        if (Move.WasPerformedThisFrame())
        {
            MoveDirection?.Invoke(Move.ReadValue<Vector2>());
        }
        if (Look.WasPerformedThisFrame())
        {
            LookingDirection?.Invoke(Look.ReadValue<Vector2>());
        }
        if (Jump.WasPerformedThisFrame())
        {
            Jumping?.Invoke();
        } 
        if (Attack.WasPerformedThisFrame())
        {
            Mepping?.Invoke();
        }
    }
}
