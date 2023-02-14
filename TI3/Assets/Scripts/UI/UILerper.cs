using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILerper : MonoBehaviour
{
    private Transform targetTransform;
    private bool isLerping = false;
    public void FixedUpdate()
    {
        if (isLerping == false) { return; }
        Vector3 distance = targetTransform.position - this.transform.position;
        if (distance.magnitude > 0.5f)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, targetTransform.position, Time.fixedUnscaledDeltaTime * 5);
        }
        else if (distance.magnitude <= 0.5f)
        {
            this.transform.position = targetTransform.position;
            isLerping = false;
        }
    }

    /// <summary>
    /// Lerps the object with this script to the desired transform position.
    /// </summary>
    /// <param name="desiredTransform"></param>
    public void OnButtonLerpPosition(Transform desiredTransform)
    {
        isLerping = true;
        targetTransform = desiredTransform;
    }
}
