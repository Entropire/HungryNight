using UnityEngine;

public class Attacking : MonoBehaviour
{
    [SerializeField] GameObject AttackBoxLeftRight;
    [SerializeField] GameObject AttackBoxUp;
    [SerializeField] GameObject AttackBoxDown;

    void FixedUpdate()
    {
        if (PlayerState.Instance.IsAttacking)
        {
            if (PlayerState.Instance.LookingDirection == new Vector2(0, 1))
            {
                AttackBoxUp.SetActive(true);
            }   
            if (PlayerState.Instance.LookingDirection == new Vector2(0, -1))
            {
                AttackBoxDown.SetActive(true);
            }
            if (PlayerState.Instance.LookingDirection == new Vector2(1, 0) || PlayerState.Instance.LookingDirection == new Vector2(-1, 0))
            {
                Debug.Log("attacking test");
                AttackBoxLeftRight.SetActive(true);
            }
        }
        else
        {
            AttackBoxLeftRight.SetActive(false);
            AttackBoxUp.SetActive(false);
            AttackBoxDown.SetActive(false);
        }
    }
}