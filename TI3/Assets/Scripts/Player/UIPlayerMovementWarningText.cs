using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayerMovementWarningText : MonoBehaviour
{
    public void Start()
    {
        Player.Instance.playerMovement.SetWarningText(GetComponent<TMP_Text>());
    }
}