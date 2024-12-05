using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

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
        MoveDirection?.Invoke(Move.IsPressed() ? Move.ReadValue<Vector2>() : Vector2.zero);

        LookingDirection?.Invoke(Look.IsPressed() ? Look.ReadValue<Vector2>() : Vector2.zero);

        if (Jump.WasPerformedThisFrame())
        {
            Jumping?.Invoke();
        } 
        if (Attack.WasPerformedThisFrame())
        {
            Mepping?.Invoke();
        }

        var trace = new InputActionTrace();

        trace.UnsubscribeFromAll();
    }
}
