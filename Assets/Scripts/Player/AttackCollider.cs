using System;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public static event Action<GameObject> OnPlayerAttacked;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnPlayerAttacked?.Invoke(other.gameObject);
    }
}
