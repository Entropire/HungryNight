using System;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public static event Action<GameObject> OnPlayerAttacked;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Attacked and hit");
        OnPlayerAttacked?.Invoke(other.gameObject);
    }
}
