using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUserInterface : MonoBehaviour
{
    [SerializeField] private GameObject userInterfaceLeft;
    [SerializeField] private GameObject userInterfaceRight;
    private Vector3 targetPosition = Vector3.zero;
    bool isLerping;
    private void Awake()
    {
        targetPosition = this.transform.position;
    }
    void Start()
    {
        userInterfaceLeft.transform.position += Vector3.left * Screen.width; // Set the transform of the left UI to the exact position on the left
        userInterfaceRight.transform.position += Vector3.right * Screen.width; // Set the transform of the right UI to the exact position on the right
    }
    public void FixedUpdate()
    {
        if (isLerping == false) { return; }
        float distance = (targetPosition - this.transform.position).magnitude; // Calculates the distance
        if (distance > 1) { this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, 5 * Time.fixedDeltaTime); } // Lerp
        else if (distance <= 1) { this.transform.position = targetPosition; isLerping = false; } // End Lerp
    }

    /// <summary>
    /// Lerps the UI to UP direction
    /// </summary>
    private void OnButtonLerpToUp()
    {
        isLerping = true;
        targetPosition.y -= Screen.height; // For some unknown reason the sign must be inverse
    }

    /// <summary>
    /// Lerps the UI to Down direction
    /// </summary>
    private void OnButtonLerpToDown()
    {
        isLerping = true;
        targetPosition.y += Screen.height; // For some unknown reason the sign must be inverse
    }

    /// <summary>
    /// Lerps the UI to Left direction
    /// </summary>
    public void OnButtonLerpToLeft()
    {
        isLerping = true;
        targetPosition.x += Screen.width; // For some unknown reason the sign must be inverse
    }

    /// <summary>
    /// Lerps the UI to Right direction
    /// </summary>
    public void OnButtonLerpToRight()
    {
        isLerping = true;
        targetPosition.x -= Screen.width; // For some unknown reason the sign must be inverse
    }
}
