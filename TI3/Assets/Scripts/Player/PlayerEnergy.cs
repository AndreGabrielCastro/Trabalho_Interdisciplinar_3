using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    public int maximumEnergy = 100;
    public float currentEnergy = 100;
    public int energyThreshold = 50;
    public int energyGeneration = 1;

    [Header("Setted during playtime")]
    public Image energyBar;

    public void SetEnergyBar(Image image)
    {
        energyBar = image;
        UpdateEnergyBar();
    }
    public void GainEnergy(float energyGain)
    {
        if (currentEnergy >= maximumEnergy) { return; }
        currentEnergy += energyGain;
        UpdateEnergyBar();
        if (currentEnergy > maximumEnergy) { currentEnergy = maximumEnergy; }
    }
    public void LoseEnergy(int energyLoss)
    {
        if (currentEnergy <= 0) { return; }
        currentEnergy -= energyLoss;
        UpdateEnergyBar();
        if (currentEnergy <= 0) { currentEnergy = 0; }
    }
    public void UpdateEnergyBar()
    {
        if (energyBar == null) { return; }
        energyBar.fillAmount = (float)currentEnergy / (float)maximumEnergy;
    }
    private void FixedUpdate()
    {
        GainEnergy(energyGeneration * Time.fixedDeltaTime);
    }
}