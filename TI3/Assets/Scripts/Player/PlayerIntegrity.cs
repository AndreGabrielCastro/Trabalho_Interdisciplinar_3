using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerIntegrity : MonoBehaviour
{
    public int maximumIntegrity = 100;
    public int currentIntegrity = 100;

    [Header("Setted during playtime")]
    public Image integrityBar; public void SetIntegrityBar(Image image) { integrityBar = image; UpdateIntegrityBar(); }
    public TMP_Text integrityAmountText; public void SetIntegrityAmountText(TMP_Text text) { integrityAmountText = text; UpdateIntegrityAmountText(); }
    public void UpdateIntegrityBar() { integrityBar.fillAmount = (float)currentIntegrity / (float)maximumIntegrity; }
    public void UpdateIntegrityAmountText() { integrityAmountText.text = $"{currentIntegrity}/{maximumIntegrity}"; }
    public void AlterateMaximumIntegrity(int value) { maximumIntegrity += value; CheckMaximumIntegrity(); UpdateIntegrityBar(); UpdateIntegrityAmountText(); }
    public void CheckMaximumIntegrity() { if (currentIntegrity > maximumIntegrity) { currentIntegrity = maximumIntegrity; } }
    public void CheckMinimumIntegrity() { if (currentIntegrity <= 0) { currentIntegrity = 0; } }
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
        CameraControllerEvent.Instance.ShakeCamera(Vector3.zero);
        if (currentIntegrity <= 0) { Die(); }
    }
    public void Die()
    {
        Instantiate(VfxSystem.Instance.vfxSpaceShipDestroyed, this.transform.position + Vector3.up * 0.1f, Quaternion.identity);
        Player.Instance.SetGameOver();
        UIGameOver.Instance.SetGameOver();
    }
}