using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIntegrity : MonoBehaviour
{
    public int maximumIntegrity = 100;
    public int currentIntegrity = 100;

    [Header("Setted during playtime")]
    public Image integrityBar;
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
        if (currentIntegrity <= 0) { Die(); }
    }
    public void UpdateIntegrityBar()
    {
        integrityBar.fillAmount = (float)currentIntegrity / (float)maximumIntegrity;
    }
    public void Die()
    {
        Instantiate(VfxSystem.Instance.vfxSpaceShipDestroyed, this.transform.position + Vector3.up * 0.1f, Quaternion.identity);
        Player.Instance.playerMovement.isGameOver = true;
        UIGameOver.Instance.SetGameOver();
    }
}