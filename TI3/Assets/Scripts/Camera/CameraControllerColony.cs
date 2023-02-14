using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerColony : MonoBehaviour
{   
    [Header("Setted during playtime - - -")]
    private Transform targetTransform;
    private bool isLerping = false;

    private void FixedUpdate()
    {
        if (isLerping == false) { return; }
        Transform playerCamera = Player.Instance.playerCameraTransform;

        playerCamera.position = Vector3.Lerp(playerCamera.position, targetTransform.position, Time.fixedDeltaTime * 3);
        playerCamera.forward = Vector3.Lerp(playerCamera.forward, targetTransform.forward, Time.fixedDeltaTime * 3);

        if ((playerCamera.position - targetTransform.position).magnitude < 0.01f)
        {
            playerCamera.position = targetTransform.position;
            playerCamera.forward = targetTransform.forward;
            isLerping = false;
        }
    }

    public void OnButtonLerpTo(Transform newTransform)
    {
        targetTransform = newTransform;
        isLerping = true;
    }
}