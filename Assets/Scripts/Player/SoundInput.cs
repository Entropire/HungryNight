using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInput : MonoBehaviour
{
    [SerializeField] List<AudioClip> PlayerSounds;
    [SerializeField] private bool LastGroundedCheck;
    private AudioSource player;
    void Start()
    {
       player = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (LastGroundedCheck)
        {
            player.clip = PlayerSounds[3];
            player.loop = false;
            if (player.clip == PlayerSounds[3] && !player.isPlaying)
            {
                player.Play();
            }
        }
        // if (PlayerState.Instance.IsWalking)
        // {
        //     player.clip = PlayerSounds[0];
        //     player.loop = true;
        //     if (player.clip == PlayerSounds[0] && !player.isPlaying)
        //     {
        //         player.Play();
        //     }
        // }else if (PlayerState.Instance.IsJumping)
        // {
        //     LastGroundedCheck = false;
        //     player.clip = PlayerSounds[1];
        //     player.loop = false;
        //     if (player.clip == PlayerSounds[1] && !player.isPlaying)
        //     {
        //         player.Play();
        //     }
        // }else if (PlayerState.Instance.IsFalling)
        // {
        //     player.clip = PlayerSounds[2];
        //     player.loop = false;
        //     if (player.clip == PlayerSounds[2] && !player.isPlaying)
        //     {
        //         player.Play();
        //     }
        // }else if (PlayerState.Instance.IsAttacking)
        // {
        //     player.clip = PlayerSounds[4];
        //     player.loop = false;
        //     if (player.clip == PlayerSounds[4] && !player.isPlaying)
        //     {
        //         player.Play();
        //     }
        // } else 
        // {
        //     player.Stop();
        // }
        
        // if (!PlayerState.Instance.IsJumping && !PlayerState.Instance.IsFalling)
        // {
        //     LastGroundedCheck = true;
        // }
        
    }
}
