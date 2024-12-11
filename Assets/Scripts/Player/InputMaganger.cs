using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMaganger : MonoBehaviour, InputSystem.IPlayerActions
{
    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("is moving");
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log("is looking");
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("is attacking");
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("is interacting");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("is jumping");
    }
}
