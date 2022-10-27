using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITaskMenuSystem : MonoBehaviour
{
    public static UITaskMenuSystem Instance;

    public Transform uiUserInterface;
    public Transform uiDeliveriesContainer;
    public UIGridObject uiDeliveryPrefab;
    public UIFloatingText uiFloatingTextPrefab;

    public TMP_Text originText;
    public TMP_Text timeText;
    public TMP_Text destinationText;
    public TMP_Text contentText;
    public TMP_Text rewardText;
    public TMP_Text conditionText;

    public UITask selectedTask;

    private void Awake()
    {
        #region ErrorTreatment
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Debug.LogError($" ----- There is more than one UITaskMenuSystem!!! ----- {this.transform.position} ----- {this.gameObject} -----");
            Destroy(this.gameObject);
            return;
        }
        #endregion
    }
    /// <summary>
    /// Updates the Task Menu Description based on the UI Task Clicked.
    /// </summary>
    /// <param name="uiTask"></param>
    public void UpdateTaskDescription(UITask uiTask)
    {
        this.selectedTask = uiTask;
        this.originText.text = uiTask.origin;
        this.destinationText.text = uiTask.destination;
        this.timeText.text = "";
        this.contentText.text = "";
        this.rewardText.text = "";
        this.conditionText.text = "";
    }

    /// <summary>
    /// Accepts the selected UITask task
    /// </summary>
    public void OnButtonAcceptTask()
    {
        if (selectedTask == null) { Debug.LogError("No Task Selected to Accept"); return; }
        selectedTask.ActivateDeliveries();
    }

    /// <summary>
    /// Try to refuse the selected UITask task
    /// </summary>
    public void OnButtonRefuseTask()
    {
        if (selectedTask == null) { Debug.LogError("No Task Selected to Refuse"); }
        selectedTask.TryDeactivateDeliveries();
    }

    /// <summary>
    /// Pops the result if the task was accepted or refused
    /// </summary>
    /// <param name="result"></param>
    public void PopResult(bool result)
    {
        UIFloatingText uiFloatingText = Instantiate(uiFloatingTextPrefab, Input.mousePosition, Quaternion.identity);
        uiFloatingText.transform.SetParent(uiUserInterface);
        uiFloatingText.transform.localScale = Vector3.one;
        TMP_Text text = uiFloatingText.GetComponent<TMP_Text>();

        if (result == false)
        {
            text.color = Color.red;
            text.text = "Remove the deliveries first!";
        }
        else if (result == true)
        {
            text.color = Color.green;
            text.text = "Sucessfully refused!";
        }
    }
}
