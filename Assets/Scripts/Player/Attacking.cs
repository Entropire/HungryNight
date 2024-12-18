using UnityEngine;

public class Attacking : MonoBehaviour
{
    [SerializeField] Collider2D AttackBox;

    void Update()
    {
        AttackBox.transform.position = transform.position +  new Vector3(
            PlayerState.Instance.LookingDirection.x * transform.localScale.x - .2f,
            PlayerState.Instance.LookingDirection.y * transform.localScale.y - .2f,
            0f
            );
        
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