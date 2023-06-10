using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerMovementSpeed : MonoBehaviour
{
    private void Start()
    {
        Player.Instance.playerMovement.SetSpeedText(this.gameObject.GetComponent<TMPro.TMP_Text>());
    }
}
