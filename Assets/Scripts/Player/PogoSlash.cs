using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoSlash : MonoBehaviour
{
    [Header("LayerMask")] 
    [SerializeField] private LayerMask HitAble; // Defines objects that can be hit
    private Rigidbody2D Player; // Reference to the player's Rigidbody2D component
    private bool IsDownSlash; // Tracks if the player is performing a down slash

    [Header("Pogo Check")] 
    private bool PogoActive; // Tracks if pogo move is active
    private bool PogoCheck; // Checks if pogo move is possible

    void Start()
    {
        Player ??= GetComponent<Rigidbody2D>(); // Assigns Rigidbody2D component if not already assigned
    }

    void Update() 
    {
        IsDownSlash = GetComponent<Attacking>().OnBoxDown(); // Checks if player is attacking downward
        Debug.DrawRay(transform.position, new Vector2(0,-6)); // Draws debug ray for visualization
        
        if (IsDownSlash && !PogoActive)
        {
            PogoActive = true; // Activates pogo move
            Vector2 pos = new Vector2(
                transform.position.x,
                transform.position.y - transform.localScale.y / 2 - 0.1f // Sets position slightly below player
            );
            
            PogoCheck = Physics2D.Raycast(pos, Vector2.down, 2, HitAble); // Checks if player can pogo off an object
            
            if (PogoCheck)
            {
                Player.velocity = new Vector2(0, 10); // Launches player upwards if pogo is possible
            }
        }
        else if (PogoActive && !IsDownSlash)
        {
            PogoActive = false; // Deactivates pogo move when down slash ends
        }
    }
}
