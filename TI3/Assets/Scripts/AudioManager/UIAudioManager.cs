using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager Instance;
    private AudioSource audioSourceSFX;
    private AudioSource audioSourceOST;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup[] audioMixerGroups;
    [SerializeField] private Slider volumeMaster;
    [SerializeField] private Slider volumeEnvironment;
    [SerializeField] private Slider volumeSoundtrack;
    [SerializeField] private Slider volumeSFX;
    [SerializeField] private Slider volumeUI;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        AudioSource[] sources = GetComponents<AudioSource>();
        audioSourceSFX = sources[0];
        audioSourceOST = sources[1];
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
        audioSourceSFX.outputAudioMixerGroup = audioMixerGroup;
    }
    public void PlaySong(AudioClip audioClip = null)
    {
        if (audioClip == null) { return; }
        audioSourceSFX.PlayOneShot(audioClip);
    }
    public void PlayMusic(AudioClip audioClip = null)
    {
        if (audioClip == null) { return; }
        audioSourceOST.clip = audioClip;
        audioSourceOST.Play();
    }
}