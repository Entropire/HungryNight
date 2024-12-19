using UnityEngine;

public class Attacking : MonoBehaviour
{
    [SerializeField] Collider2D AttackBoxLeftRight;
    [SerializeField] Collider2D AttackBoxUp;
    [SerializeField] Collider2D AttackBoxDown;

    void Update()
    {

        
        if (PlayerState.Instance.IsAttacking)
        {
            if (PlayerState.Instance.LookingDirection == new Vector2(0, -1))
            {
                AttackBoxUp.enabled = true;
            }
            if (PlayerState.Instance.LookingDirection == new Vector2(0, 1))
            {
                AttackBoxDown.enabled = true;   
            }
            if (PlayerState.Instance.LookingDirection == new Vector2(1, 0) || PlayerState.Instance.LookingDirection == new Vector2(-1, 0))
            {
                AttackBoxLeftRight.enabled = true;  
            }
        }
        else
        {
            
            AttackBoxLeftRight.enabled = false;
            AttackBoxUp.enabled = false;
            AttackBoxDown.enabled = false;
        }
    }
}