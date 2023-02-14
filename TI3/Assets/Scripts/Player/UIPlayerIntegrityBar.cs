using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerIntegrityBar : MonoBehaviour
{
    private void Start()
    {
        Player.Instance.playerIntegrity.SetIntegrityBar(this.gameObject.GetComponent<UnityEngine.UI.Image>());
    }
}