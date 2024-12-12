using HungryNight.Player;
using UnityEngine;

[RequireComponent(typeof(PlayerState))]
public class Attacking : MonoBehaviour
{
    Collider2D AttackBox;
    PlayerState playerState;
    void Start()
    {
        AttackBox = GameObject.Find("AttackBox").GetComponent<Collider2D>();
    }

    void Update()
    {
        if (playerState.IsAttacking)
        {
            AttackBox.transform.localPosition = playerState.LookingDirection;
        }
    }
}
