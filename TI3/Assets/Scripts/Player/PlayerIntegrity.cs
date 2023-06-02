using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerIntegrity : MonoBehaviour
{
    [SerializeField] private int maximumIntegrity = 100;
    [SerializeField] private int currentIntegrity = 100;
    [Header("Setted during playtime")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip alarmClip;
    [SerializeField] private Image integrityBar;
    [SerializeField] private TMP_Text integrityAmountText;
    private float alarmTimer;
    private bool isWarning;
    #region Getters&Setters
    public int GetMaxIntegrity() { return maximumIntegrity; }
    public int GetCurrentIntegrity() { return currentIntegrity; }
    public void SetMaxIntegrity(int value) { maximumIntegrity = value; }
    public void SetCurrentIntegrity(int value) { currentIntegrity = value; }
    public void SetIntegrityBar(Image image) { integrityBar = image; UpdateIntegrityBar(); }
    public void SetIntegrityAmountText(TMP_Text text) { integrityAmountText = text; UpdateIntegrityAmountText(); }
    public void SetAudioSource(AudioSource aSource) { audioSource = aSource; audioSource.clip = alarmClip; }
    #endregion
    public void UpdateIntegrityBar() { integrityBar.fillAmount = (float)currentIntegrity / (float)maximumIntegrity; }
    public void UpdateIntegrityAmountText() { integrityAmountText.text = $"{currentIntegrity}/{maximumIntegrity}"; }
    public void AlterateMaximumIntegrity(int value) { maximumIntegrity += value; CheckMaximumIntegrity(); UpdateIntegrityBar(); UpdateIntegrityAmountText(); }
    public void CheckMaximumIntegrity() { if (currentIntegrity > maximumIntegrity) { currentIntegrity = maximumIntegrity; } }
    public void CheckMinimumIntegrity() { if (currentIntegrity <= 0) { currentIntegrity = 0; } }
    
    private void PlayLowIntegrityAlarm()
    {
        if (audioSource.isPlaying) { return; }
        audioSource.Play();
        alarmTimer = 30;
        isWarning = true;
    }
    private void StopLowIntegrityAlarm()
    {
        audioSource.Stop();
        isWarning = false;
    }
    public void HealDamage(int heal)
    {
        if (currentIntegrity >= maximumIntegrity) { return; }
        currentIntegrity += heal;
        CheckMaximumIntegrity();
        UpdateIntegrityBar();
        UpdateIntegrityAmountText();
    }
    public void TakeDamage(int damage)
    {
        if (currentIntegrity <= 0) { return; }
        currentIntegrity -= damage;
        UpdateIntegrityBar();
        UpdateIntegrityAmountText();
        if (currentIntegrity <= maximumIntegrity * 0.25)
        {
            PlayLowIntegrityAlarm();
        }
        CameraControllerEvent.Instance.ShakeCamera(Vector3.zero);
        if (currentIntegrity <= 0) { Die(); }
    }
    public void Die()
    {
        Instantiate(Player.Instance.playerFXs.GetVFXSpaceShipDestroyed(), this.transform.position + Vector3.up * 0.1f, Quaternion.identity);
        Player.Instance.SetGameOver();
    }
    private void FixedUpdate()
    {
        if (isWarning == false) { return; }
        alarmTimer -= Time.fixedDeltaTime;
        if (alarmTimer < 0) { StopLowIntegrityAlarm(); }
    }
}