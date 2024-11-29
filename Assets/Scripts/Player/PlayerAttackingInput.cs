using System;
using System.Collections;
using System.Collections.Generic;
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
