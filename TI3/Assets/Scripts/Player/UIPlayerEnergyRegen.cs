using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerEnergyRegen : MonoBehaviour
{
    private void Start()
    {
        Player.Instance.playerEnergy.SetEnergyRegenText(this.gameObject.GetComponent<TMPro.TMP_Text>());
    }
}