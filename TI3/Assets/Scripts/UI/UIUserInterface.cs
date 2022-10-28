using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUserInterface : MonoBehaviour
{
    public static UIUserInterface Instance;

    public UIFloatingText uiFloatingTextPrefab;

    [SerializeField] private GameObject userInterfaceUp;
    [SerializeField] private GameObject userInterfaceDown;
    [SerializeField] private GameObject userInterfaceLeft;
    [SerializeField] private GameObject userInterfaceRight;

    private Vector3 targetPosition = Vector3.zero;
    bool isLerping;
    private void Awake()
    {
        #region ErrorTreatment
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Debug.LogError($" ----- There is more than one UIUserInterface!!! ----- {this.transform.position} ----- {this.gameObject} -----");
            Destroy(this.gameObject);
            return;
        }
        #endregion
        targetPosition = this.transform.position;
    }
    void Start()
    {
        userInterfaceUp.transform.position = this.transform.position + Vector3.up * Screen.height; // Set the transform of the Up UI to the exact position on the up
        userInterfaceDown.transform.position = this.transform.position + Vector3.down * Screen.height;// Set the transform of the Down UI to the exact position on the down
        userInterfaceLeft.transform.position = this.transform.position + Vector3.left * Screen.width; // Set the transform of the Left UI to the exact position on the left
        userInterfaceRight.transform.position = this.transform.position + Vector3.right * Screen.width; // Set the transform of the Right UI to the exact position on the right
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
    public void OnButtonLerpToUp()
    {
        isLerping = true;
        targetPosition.y -= Screen.height; // For some unknown reason the sign must be inverse
    }

    /// <summary>
    /// Lerps the UI to Down direction
    /// </summary>
    public void OnButtonLerpToDown()
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
    public void PopResult(string text, Color color)
    {
        UIFloatingText uiFloatingText = Instantiate(uiFloatingTextPrefab, Input.mousePosition, Quaternion.identity);
        uiFloatingText.transform.SetParent(this.transform);
        uiFloatingText.transform.localScale = Vector3.one;
        TMP_Text floatingText = uiFloatingText.GetComponent<TMP_Text>();
        floatingText.color = color;
        floatingText.text = text;
    }
}
