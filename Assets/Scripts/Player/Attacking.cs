using UnityEngine;

public class Attacking : MonoBehaviour
{
    [SerializeField] Collider2D AttackBoxLeftRight;
    [SerializeField] Collider2D AttackBoxUp;
    [SerializeField] Collider2D AttackBoxDown;
    PlayerState playerState;
    
    void Start()
    {
        playerState = GetComponent<PlayerState>();
    }
    void Update()
    {

        
        if (playerState.IsAttacking)
        {
            if (playerState.LookingDirection == new Vector2(0, 1))
            {
                AttackBoxUp.enabled = true;
            }
            if (playerState.LookingDirection == new Vector2(0, -1))
            {
                AttackBoxDown.enabled = true;   
            }
            if (playerState.LookingDirection == new Vector2(1, 0) || playerState.LookingDirection == new Vector2(-1, 0))
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

    public bool OnBoxDown()
    {
         return AttackBoxDown.enabled;
    }
}