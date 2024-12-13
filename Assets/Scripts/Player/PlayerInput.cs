using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;
    public static event Action<Vector2> LookingDirection;
    public static event Action IsWalking;
    public static event Action Attack;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack?.Invoke();
            print(this + "Attack");
        }
        switch (Input.inputString.ToLower())
        {
            case "w":
                LookingDirection?.Invoke(Vector2.up);
                print(this + "Up");

                break;
            case "a":
                LookingDirection?.Invoke(Vector2.left);
                print(this + "Left");

                IsWalking?.Invoke();
                break;
            case "d":
                LookingDirection?.Invoke(Vector2.right);
                IsWalking?.Invoke();
                print(this + "Right");

                break;
            case "s":
                LookingDirection?.Invoke(Vector2.down);
                print(this + "Down");

                break;
            default:
                break;
        }
    }
}
