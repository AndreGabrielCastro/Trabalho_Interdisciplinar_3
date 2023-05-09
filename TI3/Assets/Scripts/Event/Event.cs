using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event : MonoBehaviour
{
    [SerializeField] private AudioClip soundtrack;
    public void PlaySoundTrack()
    {
        UIAudioManager.Instance.PlayMusic(soundtrack);
    }
}