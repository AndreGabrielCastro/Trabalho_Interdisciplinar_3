using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILerper : MonoBehaviour
{
    public Transform disabledTransform;
    public Transform enabledTransform;
    private Transform targetTransform;
    private bool isEnabled = false;
    private bool isLerping = false;
    private void Start()
    {
        isEnabled = false;
        this.transform.position = disabledTransform.position;
    }
    public void FixedUpdate()
    {
        if (isLerping == false) { return; }
        Vector3 distance = targetTransform.position - this.transform.position;
        if (distance.magnitude > 0.5f)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, targetTransform.position, Time.fixedDeltaTime * 5);
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
        if (isEnabled == true)
        {
            isLerping = true;
            isEnabled = false;
            targetTransform = disabledTransform;
        }
        else if (isEnabled == false)
        {
            isLerping = true;
            isEnabled = true;
            targetTransform = enabledTransform;
        }
    }
}
