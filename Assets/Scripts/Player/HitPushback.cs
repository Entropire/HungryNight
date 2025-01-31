using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPushback : MonoBehaviour
{
    [Header("Hit Forces")] 
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private int YhitCords = 4; // Default Y-axis hit force
    private bool isntFallSpike; // Determines if the spike is a falling spike or not
    private PlayerMovement playMovement; // Reference to the PlayerMovement script
    [SerializeField] private List<Vector3> LastPos; // Stores last known positions

    [Header("Fade To Black")] 
    [SerializeField] private Image Blackfade; // UI element for fade effect
    [SerializeField] private Color BaseColor; // Base color for fade effect
    [SerializeField] private Color ToColor; // Target color for fade effect

    [Header("Hit Timing")] 
    [SerializeField] private float Hittime = 0.03f; // Short hit time duration
    [SerializeField] private float hitCooldowntime = 1f; // Cooldown time after getting hit
    private bool ishitCooldown; // Flag to check if in hit cooldown
    private float TimeElepsed; // Tracks elapsed time during cooldown

    void Start()
    {
        Blackfade.color = BaseColor; // Set initial fade color to base color
        ToColor = BaseColor; // Initialize ToColor to base color
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component
        playMovement = GetComponent<PlayerMovement>(); // Get PlayerMovement component
    }

    // Update is called once per frame
    void Update()
    {
        if (ishitCooldown)
        {
            TimeElepsed += Time.deltaTime; // Increment elapsed time

            if (TimeElepsed > Hittime)
            {
                Time.timeScale = 1; // Reset time scale after hit effect
            }
            
            if (TimeElepsed > 0.7f)
            {
                if (isntFallSpike)
                {
                    transform.position = LastPos[0]; // Reset player position if not a fall spike
                    rb.velocity = new Vector3(0, 0, 0); // Stop movement
                }
            }
            
            if (TimeElepsed > 0.75f)
            {
                playMovement.enabled = true; // Re-enable player movement
            }
            
            if (TimeElepsed > hitCooldowntime)
            {
                ToColor = BaseColor; // Reset fade color
                ishitCooldown = false; // End cooldown
                TimeElepsed = 0; // Reset elapsed time
            }
        }
        
        // Smoothly transition the fade effect
        Blackfade.color = Color.Lerp(Blackfade.color, ToColor, 1f * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Spike")) // Check if player collides with a spike
        {
            Vector2 direction = (transform.position - other.gameObject.transform.position).normalized; // Get direction of impact
            
            if (!ishitCooldown)
            {
                if (direction.y > 0 && other.GetComponent<SpikeFall>() == null) // Check if hit from below and not a falling spike
                {
                    YhitCords = 32; // Set a higher Y force if hit from below
                    ToColor = Color.black; // Change fade effect to black
                    isntFallSpike = true; // Mark as non-falling spike hit
                }
                else
                {
                    YhitCords = 18; // Lower Y force otherwise
                    ToColor = BaseColor; // Keep fade effect as base color
                    isntFallSpike = false; // Mark as falling spike hit
                }

                // Apply knockback force based on hit direction
                if (direction.x > 0)
                {
                    rb.AddForce(new Vector2(4, YhitCords), ForceMode2D.Impulse); // Push right
                }
                else 
                {
                    rb.AddForce(new Vector2(-4, YhitCords), ForceMode2D.Impulse); // Push left
                }
                
                Time.timeScale = 0.1f; // Slow down time briefly for effect
            }
            
            ishitCooldown = true; // Activate hit cooldown
            playMovement.enabled = false; // Disable player movement temporarily
        }
    }

}
