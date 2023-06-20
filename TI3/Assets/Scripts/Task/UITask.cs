using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITask : MonoBehaviour
{
    [HideInInspector] public bool isAccepted;

    [Header("Setted during playtime")]
    public Task task;
    public Image imageState;

    public GridObjectDelivery[] taskGridObjectDeliveryArray;
    public UIGridObjectDelivery[] taskUiGridObjectDeliveryArray;

    /// <summary>
    /// Generate all Task's attributes
    /// </summary>
    /// <param name="currentColony"></param>
    public void GenerateTask(Colony currentColony)
    {
        // This body of code creates all non-object Task attributes.
        string taskOrigin = currentColony.colonyName; // Sets the origin of the Tasks posteriorly created
        string taskDestination = currentColony.associatedColonyArray[Random.Range(0, currentColony.associatedColonyArray.Length)]; // Sets the destination of the Task based on the amount of associated colonies
        int taskContentAmount = Random.Range(currentColony.contentMinAmountPerTask, currentColony.contentMaxAmountPerTask + 1); // Determines the amount of content to be created by the Task
        int taskGearcoinAmount = Random.Range(currentColony.colonyMinPayment, currentColony.colonyMaxPayment) * taskContentAmount;
        int taskInformationAmount = Random.Range(currentColony.colonyMinPayment, currentColony.colonyMaxPayment) * taskContentAmount;
        int taskTime = 3 * taskContentAmount;
        string taskRewardDescription = $"{taskGearcoinAmount} gearcoins\n{taskInformationAmount} information"; // Sets the Task's reward description
        taskGridObjectDeliveryArray = new GridObjectDelivery[taskContentAmount]; // Sets the Task's Grid Object Deliveries array size
        taskUiGridObjectDeliveryArray = new UIGridObjectDelivery[taskContentAmount]; // Sets the Task's UI Grid Object Deliveries array size

        string taskContentDescription = "";
        // This body of code creates the object Task attributes foreach Grid Object Delivery 
        for (int i = 0; i < taskGridObjectDeliveryArray.Length; i++)
        {
            int random = Random.Range(0, UITaskMenuSystem.Instance.allGridObjectDeliveryPrefabArray.Length); // Choose one over all of the grid object deliveries available
            GridObjectDelivery gridObjectDelivery = Instantiate(UITaskMenuSystem.Instance.allGridObjectDeliveryPrefabArray[random], Vector3.zero, Quaternion.identity); // Gets the grid object
            taskContentDescription += $"{gridObjectDelivery.description}\n";
            taskGridObjectDeliveryArray[i] = gridObjectDelivery; // Stores the grid object on the array

            UIGridObjectDelivery uiGridObjectDelivery = Instantiate(UITaskMenuSystem.Instance.uiDeliveryPrefab, Vector3.zero, Quaternion.identity); // Instantiates the UI of the gridObject
            uiGridObjectDelivery.SetGridObjectDelivery(gridObjectDelivery); // Sets the prefab of the UI grid object
            uiGridObjectDelivery.transform.SetParent(UITaskMenuSystem.Instance.uiDeliveriesContainer); // Sets the transform of the UI grid object
            uiGridObjectDelivery.transform.localScale = Vector3.one; // Resets the scale of the UI grid object
            uiGridObjectDelivery.gameObject.SetActive(false); // Deactivates the UI grid object
            taskUiGridObjectDeliveryArray[i] = uiGridObjectDelivery; // Stores the UI grid object
        }

        // Sets the Task
        task = new Task(taskOrigin, taskTime, taskDestination, taskContentAmount,
                        taskGearcoinAmount, taskInformationAmount,
                        taskContentDescription, taskRewardDescription,
                        taskGridObjectDeliveryArray, taskUiGridObjectDeliveryArray);
    }

    /// <summary>
    /// Activates the Task's UI Grid Object Deliveries
    /// </summary>
    public bool TryActivateDeliveries()
    {
        if (isAccepted == true) { return false; }
        ActivateDeliveries();
        return true;
    }
    private void ActivateDeliveries()
    {
        for (int i = 0; i < taskUiGridObjectDeliveryArray.Length; i++) // This "for" goes through all Task's UI Grid Object Deliveries in the UI Task
        { taskUiGridObjectDeliveryArray[i].gameObject.SetActive(true); } // Activate the UI Grid Object
        imageState.color = new Color(0.3f, 1f, 0.6f, 0.8f); // Sets the Image State to green
        PlayerSystem.Instance.taskList.Add(this.task); // Adds the Task to the list
        this.isAccepted = true; // Marks it as accepted
    }

    /// <summary>
    /// Tries to deactivate the Task's UI Grid Object Deliveries.
    /// </summary>
    public void TryDeactivateDeliveries()
    {
        for (int i = 0; i < taskUiGridObjectDeliveryArray.Length; i++) // This "for" goes through all Task's UI Grid Object Deliveries in the UI Task
        {
            if (taskUiGridObjectDeliveryArray[i].isPlaced == true) // If the UI Grid Object is placed...
            { UIUserInterface.Instance.PopResult("Remove the task's deliveries first!", Color.red); return; } // Pops NO and returns
        }
        DeactivateDeliveries(); // Deactivates the Deliveries
    }

    /// <summary>
    /// Deactivates the Task's UI Grid Object Deliveries
    /// </summary>
    private void DeactivateDeliveries()
    {
        for (int i = 0; i < taskUiGridObjectDeliveryArray.Length; i++) // This "for" goes through all Task's UI Grid Object Deliveries in the UI Task
        { taskUiGridObjectDeliveryArray[i].gameObject.SetActive(false); } // Deactivates the UI Grid Object
        imageState.color = new Color(1f, 0.4f, 0.5f, 0.8f); // Sets the Image State to Red
        PlayerSystem.Instance.taskList.Remove(this.task); // Removes the Task of the list
        this.isAccepted = false; // Marks it as unaccepted
    }

    /// <summary>
    /// Updates the Task Description based on the clicked UI Task.
    /// </summary>
    public void OnClickRequestTaskDescriptionUpdate()
    {
        UITaskMenuSystem.Instance.UpdateTaskDescription(this);
    }
}