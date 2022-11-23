using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraEventLookAt : MonoBehaviour
{
    [Header("Setted during playtime")]
    public bool isEventRunning;
    private void FixedUpdate()
    {
        if (isEventRunning == false) { return; }
        Vector3 pivotPosition = Player.Instance.transform.position;
        Vector3 mousePosition = MouseSystem.Instance.GetWorldPosition();
        this.transform.position = Vector3.ClampMagnitude(mousePosition, 10 + pivotPosition.magnitude);
    }
}
