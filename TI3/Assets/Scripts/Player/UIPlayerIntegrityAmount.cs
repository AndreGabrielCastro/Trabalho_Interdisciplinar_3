using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerIntegrityAmount : MonoBehaviour
{
    private void Start()
    {
        Player.Instance.playerIntegrity.SetIntegrityAmountText(this.gameObject.GetComponent<TMPro.TMP_Text>());
    }
}