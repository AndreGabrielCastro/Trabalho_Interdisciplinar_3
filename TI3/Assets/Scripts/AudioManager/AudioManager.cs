using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isPlaying;

    public void PlaySong(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}