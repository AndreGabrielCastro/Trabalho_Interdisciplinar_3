using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerEnergyAmount : MonoBehaviour
{
    private void Start()
    {
        Player.Instance.playerEnergy.SetEnergyAmountText(this.gameObject.GetComponent<TMPro.TMP_Text>());
    }
}
