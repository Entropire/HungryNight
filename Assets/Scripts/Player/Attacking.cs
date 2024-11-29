using UnityEngine;
using StateMachine;

public class Attacking : PlayerState
{
    Transform AttackBox;
    void Start()
    {
        AttackBox = GameObject.Find("AttackBox").transform;
    }

    void Update()
    {
        if (instance.IsAttacking)
        {
            AttackBox.localPosition = instance.LookingDirection;
        }
    }
}
