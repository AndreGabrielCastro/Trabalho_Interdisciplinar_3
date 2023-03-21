using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEnergy : MonoBehaviour
{
    public int maximumEnergy = 100;
    public float currentEnergy = 100;
    public int energyThreshold = 50;
    public float energyGeneration = 1;

    [Header("Setted during playtime")]
    public Image energyBar; public void SetEnergyBar(Image image) { energyBar = image; UpdateEnergyBar(); }
    public TMP_Text energyAmountText; public void SetEnergyAmountText(TMP_Text text) { energyAmountText = text; UpdateEnergyAmountText(); }
    public TMP_Text energyRegenText; public void SetEnergyRegenText(TMP_Text text) { energyRegenText = text; UpdateEnergyRegenText(); }
    public void UpdateEnergyBar() { if (energyBar == null) { return; } energyBar.fillAmount = (float)currentEnergy / (float)maximumEnergy; }
    public void UpdateEnergyAmountText() { if (energyAmountText == null) { return; } energyAmountText.text = $"{(int)currentEnergy}/{maximumEnergy}"; }
    public void UpdateEnergyRegenText() { if (energyRegenText == null) { return; } energyRegenText.text = $"{energyGeneration}/s"; }
    public void AlterateMaximumEnergy(int value) { maximumEnergy += value; CheckMaximumEnergy(); UpdateEnergyBar(); UpdateEnergyAmountText(); }
    public void AlterateEnergyGeneration(float value) { energyGeneration += value; UpdateEnergyRegenText(); }
    public void CheckMaximumEnergy() { if (currentEnergy > maximumEnergy) { currentEnergy = maximumEnergy; } }
    public void CheckMinimumEnergy() { if (currentEnergy <= 0) { currentEnergy = 0; } }
    public void GainEnergy(float energyGain)
    {
        if (currentEnergy >= maximumEnergy) { return; }
        currentEnergy += energyGain;
        CheckMaximumEnergy();
        UpdateEnergyBar();
        UpdateEnergyAmountText();
    }
    
    public void LoseEnergy(int energyLoss)
    {
        if (currentEnergy <= 0) { return; }
        currentEnergy -= energyLoss;
        CheckMinimumEnergy();
        UpdateEnergyBar();
        UpdateEnergyAmountText();
    }
    private void FixedUpdate()
    {
        GainEnergy(energyGeneration * Time.fixedDeltaTime);
    }
}