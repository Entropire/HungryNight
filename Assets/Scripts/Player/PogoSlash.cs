using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoSlash : MonoBehaviour
{
    [SerializeField] private LayerMask HitAble;
    private Rigidbody2D Player;
    private bool IsDownSlash;
    [SerializeField] private bool PogoActive;
    private bool PogoCheck;
    void Start()
    {
        Player ??= GetComponent<Rigidbody2D>();
    }
    void Update() 
    {
        IsDownSlash = GetComponent<Attacking>().OnBoxDown();
        Debug.DrawRay(transform.position, new Vector2(0,-6));   
        if (IsDownSlash && !PogoActive)
        {
            PogoActive = true;
            Vector2 pos = new Vector2(
            transform.position.x,
            transform.position.y - transform.localScale.y / 2 - .1f
            );
            PogoCheck = Physics2D.Raycast(pos, Vector2.down, 2, HitAble);
            
            if (PogoCheck)
            {
                Player.velocity = new Vector2(0, 10);
            }
        }else if (PogoActive && !IsDownSlash)
        {
            PogoActive = false;
        }
        
    }
}
