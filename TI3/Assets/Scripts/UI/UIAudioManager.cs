using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIAudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioMixer audioMixer;
    public AudioMixerGroup[] audioMixerGroups;
    public Slider volumeMaster;
    public Slider volumeEnvironment;
    public Slider volumeSoundtrack;
    public Slider volumeSFX;
    public Slider volumeUI;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        volumeMaster.value = PlayerSystem.Instance.masterVolumeValue;
        volumeEnvironment.value = PlayerSystem.Instance.environmentVolumeValue;
        volumeSoundtrack.value = PlayerSystem.Instance.soundtrackVolumeValue;
        volumeSFX.value = PlayerSystem.Instance.sfxVolumeValue;
        volumeUI.value = PlayerSystem.Instance.uiVolumeValue;
    }
    public void OnButtonSetMasterVolume()
    {
        audioMixer.SetFloat("MasterVolume", volumeMaster.value);
        PlayerSystem.Instance.masterVolumeValue = (sbyte)volumeMaster.value;
    }
    public void OnButtonSetEnvironmentVolume()
    {
        audioMixer.SetFloat("EnvironmentVolume", volumeEnvironment.value);
        PlayerSystem.Instance.environmentVolumeValue = (sbyte)volumeEnvironment.value;
    }
    public void OnButtonSetSoundtrackVolume()
    {
        audioMixer.SetFloat("SoundtrackVolume", volumeSoundtrack.value);
        PlayerSystem.Instance.soundtrackVolumeValue = (sbyte)volumeSoundtrack.value;
    }
    public void OnButtonSetSFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", volumeSFX.value);
        PlayerSystem.Instance.sfxVolumeValue = (sbyte)volumeSFX.value;
    }
    public void OnButtonSetUIVolume()
    {
        audioMixer.SetFloat("UIVolume", volumeUI.value);
        PlayerSystem.Instance.uiVolumeValue = (sbyte)volumeUI.value;
    }
    public void OnButtonSetAudioMixerGroup(AudioMixerGroup audioMixerGroup = null)
    {
        if (audioMixerGroup == null) { return; }
        audioSource.outputAudioMixerGroup = audioMixerGroup;
    }
    public void OnButtonPlaySong(AudioClip audioClip = null)
    {
        if (audioClip == null) { return; }
        audioSource.PlayOneShot(audioClip);
    }
}