using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource oneShotAudioSource;
    public void SetAudioRelated(AudioSource aSource)
    {
        oneShotAudioSource = aSource;
    }
    public void PlaySong(AudioClip clip)
    {
        oneShotAudioSource.PlayOneShot(clip);
    }
}