using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerEnergyBar : MonoBehaviour
{
    private void Start()
    {
        Player.Instance.playerEnergy.SetEnergyBar(this.gameObject.GetComponent<UnityEngine.UI.Image>());
    }
}