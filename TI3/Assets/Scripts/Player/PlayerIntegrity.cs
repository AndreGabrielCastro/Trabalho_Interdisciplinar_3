using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIntegrity : MonoBehaviour
{
    public Image integrityBar;
    public int maximumIntegrity = 100;
    public int currentIntegrity = 100;
    public void HealDamage(int heal)
    {
        if (currentIntegrity >= maximumIntegrity) { return; }
        currentIntegrity += heal;
        UpdateIntegrityBar();
    }
    public void TakeDamage(int damage)
    {
        if (currentIntegrity <= 0) { return; }
        currentIntegrity -= damage;
        UpdateIntegrityBar();
    }
    public void UpdateIntegrityBar()
    {
        integrityBar.fillAmount = (float)currentIntegrity / (float)maximumIntegrity;
    }
}