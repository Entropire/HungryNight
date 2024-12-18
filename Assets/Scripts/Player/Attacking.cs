using UnityEngine;

public class Attacking : MonoBehaviour
{
    [SerializeField] Collider2D AttackBox;

    void Update()
    {
        if (PlayerState.Instance.IsAttacking)
        {
            AttackBox.enabled = true;
        }
        else
        {
            AttackBox.enabled = false;
        }
    }
}
