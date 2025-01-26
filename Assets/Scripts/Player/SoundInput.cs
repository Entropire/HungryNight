using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInput : MonoBehaviour
{
    [SerializeField] List<AudioClip> PlayerSounds;
    private AudioSource player;
    private PlayerState playerState;
    private bool hasPlayed = false;
    private bool lastGroundedCheck = true;

    void Start()
    {
        player = GetComponent<AudioSource>();
        playerState = GetComponent<PlayerState>();
    }

    void Update()
    {
        UpdatePlayerSound();
    }

    public void UpdatePlayerSound()
    {
        if (lastGroundedCheck && !player.isPlaying)
        {
            hasPlayed = false;
        }

        if (playerState.IsWalking && !playerState.IsJumping && !playerState.IsFalling)
        {
            PlaySound(0, true);
            player.volume = 1;
        }
        else if (playerState.IsJumping)
        {
            lastGroundedCheck = false;
            PlaySoundOnce(1);
            player.volume = 0.3f;
        }
        else if (playerState.IsFalling && !playerState.IsJumping )
        {
            PlaySoundOnce(2);
            player.volume = 0.3f;
        }
        else if (playerState.IsAttacking)
        {
            PlaySound(4, false);
            player.volume = 1;
        }
        else
        {
            StopSound();
        }

        if (!playerState.IsJumping && !playerState.IsFalling)
        {
            lastGroundedCheck = true;
        }
    }

    private void PlaySound(int soundIndex, bool loop)
    {
        hasPlayed = false;
        player.clip = PlayerSounds[soundIndex];
        player.loop = loop;
        if (!player.isPlaying)
        {
            player.Play();
        }
    }

    private void PlaySoundOnce(int soundIndex)
    {
        if (!hasPlayed)
        {
            player.clip = PlayerSounds[soundIndex];
            player.loop = false;
            if (!player.isPlaying)
            {
                hasPlayed = true;
                player.Play();
            }
        }
    }

    private void StopSound()
    {
        if (player.isPlaying)
        {
            player.Stop();
        }
    }
}
