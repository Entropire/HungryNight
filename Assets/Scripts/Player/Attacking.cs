using UnityEngine;
using StateMachine;

public class Attacking : PlayerState
{
    Collider2D AttackBox;
    void Start()
    {
        AttackBox = GameObject.Find("AttackBox").GetComponent<Collider2D>();
    }

    void Update()
    {
        if (instance.IsAttacking)
        {
            AttackBox.transform.localPosition = instance.LookingDirection;
            
        }
    }
}
