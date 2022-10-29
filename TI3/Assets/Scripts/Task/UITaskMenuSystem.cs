using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITaskMenuSystem : MonoBehaviour
{
    public static UITaskMenuSystem Instance;

    [Header("Task related")]
    public Transform uiTasksContainer;
    public UITask uiTaskPrefab;

    [Header("Delivery related")]
    public Transform uiDeliveriesContainer;
    public UIGridObjectDelivery uiDeliveryPrefab;

    [Header("All Content related")]
    public GridObjectDelivery[] allGridObjectDeliveryPrefabArray;

    [Header("Description related")]
    public TMP_Text originText;
    public TMP_Text timeText;
    public TMP_Text destinationText;
    public TMP_Text contentText;
    public TMP_Text rewardText;
    public TMP_Text conditionText;

    [Header("Setted during playtime")]
    public UITask selectedTask;
    public UITask[] uiTaskArray;

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
        this.originText.text = uiTask.taskOrigin;
        this.destinationText.text = uiTask.taskDestination;
        this.timeText.text = "";
        this.contentText.text = uiTask.taskDescription;
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
        UIFloatingText uiFloatingText = Instantiate(UIUserInterface.Instance.uiFloatingTextPrefab, Input.mousePosition, Quaternion.identity);
        uiFloatingText.transform.SetParent(UIUserInterface.Instance.transform);
        uiFloatingText.transform.localScale = Vector3.one;
        TMP_Text floatingText = uiFloatingText.GetComponent<TMP_Text>();
        floatingText.color = color;
        floatingText.text = text;
    }

    public void GenerateTasks()
    {
        Colony currentColony = ColonySystem.Instance.allColoniesArray[ColonySystem.Instance.currentColonyIndex]; // Gets the current colony from the All Colonies Array
        int taskAmount = Random.Range(currentColony.taskMinAmount, currentColony.taskMaxAmount + 1); // Determines the new size of the UI task array
        this.uiTaskArray = new UITask[taskAmount]; // Sets the new size of the UI task array

        for (int i = 0; i < taskAmount; i++)
        {
            UITask uiTask = Instantiate(uiTaskPrefab, Vector3.zero, Quaternion.identity); // Creates the UI task
            uiTask.transform.SetParent(uiTasksContainer); // Sets it as child of the UI task container
            uiTask.transform.localScale = Vector3.one; // Sets it's scale to 1
            uiTask.SetTask(currentColony); // Sets the atributes to the UI task
            uiTaskArray[i] = uiTask; // Stores it in the array
        }
    }
}
