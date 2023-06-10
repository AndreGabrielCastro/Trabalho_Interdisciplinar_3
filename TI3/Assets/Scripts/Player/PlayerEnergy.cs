using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] private int maximumEnergy = 100;
    [SerializeField] private float currentEnergy = 100;
    [SerializeField] private int energyThreshold = 50;
    [SerializeField] private float energyGeneration = 1;

    [Header("Setted during playtime")]
    [SerializeField] private Image energyBar;
    [SerializeField] private TMP_Text energyAmountText;
    [SerializeField] private TMP_Text energyRegenText; 
    public float GetEnergyGeneration() { return energyGeneration; }
    public void SetEnergyBar(Image image) { energyBar = image; UpdateEnergyBar(); }
    public void SetEnergyAmountText(TMP_Text text) { energyAmountText = text; UpdateEnergyAmountText(); }
    public void SetEnergyRegenText(TMP_Text text) { energyRegenText = text; UpdateEnergyRegenText(); }
    public float GetCurrentEnergy() { return currentEnergy; }
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
    
    public void LoseEnergy(float energyLoss)
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