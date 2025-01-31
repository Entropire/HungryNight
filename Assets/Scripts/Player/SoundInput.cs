using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInput : MonoBehaviour
{
    [SerializeField] List<AudioClip> PlayerSounds; // List of audio clips for different player actions
        private AudioSource player; // AudioSource component to play sounds
        private PlayerState playerState; // Reference to the PlayerState script
        private bool hasPlayed = false; // Flag to check if a sound has already played once
        private bool lastGroundedCheck = true; // Tracks if the player was last grounded
        PogoSlash pogoSlash; // Reference to the PogoSlash script
    
        void Start()
        {
            player = GetComponent<AudioSource>(); // Get the AudioSource component attached to the player
            playerState = GetComponent<PlayerState>(); // Get the PlayerState component
            pogoSlash = GetComponent<PogoSlash>(); // Get the PogoSlash component
        }
    
        void Update()
        {
            UpdatePlayerSound(); // Call the function to update the player sound state
        }
    
        public void UpdatePlayerSound()
        {
            // Reset hasPlayed flag if the player was last grounded and no sound is playing
            if (lastGroundedCheck && !player.isPlaying)
            {
                hasPlayed = false;
            }
    
            // Play walking sound if the player is walking and not jumping or falling
            if (playerState.IsWalking && !playerState.IsJumping && !playerState.IsFalling)
            {
                PlaySound(0, true);
                player.volume = 1;
            }
            // Play jumping sound if the player is jumping and not performing a pogo attack
            else if (playerState.IsJumping && !pogoSlash.CheckPogo())
            {
                lastGroundedCheck = false; // Player is airborne, update last grounded check
                PlaySoundOnce(1);
                player.volume = 0.3f;
            }
            // Play falling sound if the player is falling but not jumping or pogoing
            else if (playerState.IsFalling && !playerState.IsJumping && !pogoSlash.CheckPogo())
            {
                PlaySoundOnce(2);
                player.volume = 0.3f;
            }
            // Play attacking sound if the player is attacking and not pogoing
            else if (playerState.IsAttacking && !pogoSlash.CheckPogo())
            {
                PlaySound(4, false);
                player.volume = 1;
            } 
            // Play pogo attack sound if the player is pogoing
            else if (pogoSlash.CheckPogo())
            {
                PlaySound(5, false);
                player.volume = 1f;
            }
            // Stop playing sound if no action is being performed
            else
            {
                StopSound();
            }
    
            // If the player is neither jumping nor falling, update the last grounded check
            if (!playerState.IsJumping && !playerState.IsFalling)
            {
                lastGroundedCheck = true;
            }
        }
    
        // Plays a looping or non-looping sound based on the given sound index
        private void PlaySound(int soundIndex, bool loop)
        {
            hasPlayed = false; // Reset hasPlayed flag
            player.clip = PlayerSounds[soundIndex]; // Assign the correct audio clip
            player.loop = loop; // Set looping behavior
            if (!player.isPlaying)
            {
                player.Play(); // Play the sound if it's not already playing
            }
        }
    
        // Plays a sound once, ensuring it doesn't repeat within the same action
        private void PlaySoundOnce(int soundIndex)
        {
            if (!hasPlayed)
            {
                player.clip = PlayerSounds[soundIndex]; // Assign the correct audio clip
                player.loop = false; // Ensure the sound does not loop
                if (!player.isPlaying)
                {
                    hasPlayed = true; // Mark the sound as played
                    player.Play(); // Play the sound
                }
            }
        }
    
        // Stops the currently playing sound
        private void StopSound()
        {
            if (player.isPlaying)
            {
                player.Stop();
            }
        }
}
