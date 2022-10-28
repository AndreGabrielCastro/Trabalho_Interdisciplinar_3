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
        if (selectedTask == null) { UIUserInterface.Instance.PopResult("Select a task first!", Color.red); return; }
        selectedTask.ActivateDeliveries();
    }

    /// <summary>
    /// Try to refuse the selected UITask task
    /// </summary>
    public void OnButtonRefuseTask()
    {
        if (selectedTask == null) { UIUserInterface.Instance.PopResult("Select a task first!", Color.red); return; }
        selectedTask.TryDeactivateDeliveries();
    }

    /// <summary>
    /// Pops the result if the task was accepted or refused
    /// </summary>
    /// <param name="result"></param>
    public void PopResult(string text, Color color)
    {
        UIFloatingText uiFloatingText = Instantiate(uiFloatingTextPrefab, Input.mousePosition, Quaternion.identity);
        uiFloatingText.transform.SetParent(uiUserInterface);
        uiFloatingText.transform.localScale = Vector3.one;
        TMP_Text floatingText = uiFloatingText.GetComponent<TMP_Text>();
        floatingText.color = color;
        floatingText.text = text;
    }
}
