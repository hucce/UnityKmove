using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSound : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();

    public enum clip
    {
        Idle, Attack, Hit
    }

    public void PlaySound(clip _clip)
    {
        var audioSource = GetComponent<AudioSource>();

        audioSource.clip = audioClips[((int)_clip)];
        if (_clip == clip.Idle)
        {
            audioSource.loop = true;
        }
        else
        {
            audioSource.loop = false;
        }

        if (audioSource.isPlaying == false)
        {
            audioSource.Play();
        }
    }
}
