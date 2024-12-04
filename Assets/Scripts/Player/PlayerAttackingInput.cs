using System;
using UnityEngine;

public class PlayerAttackingInput : MonoBehaviour
{
    public static event Action Attacked;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attacked?.Invoke();
        }
    }
}
