using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITask : MonoBehaviour
{
    [Header("Setted during playtime")]
    public Task task;
    public Image imageState;

    public string taskOrigin;
    public string taskDestination;
    public int taskContentAmount;
    public int taskGearcoinAmount;
    public int taskInformationAmount;
    public string taskDescription;
    public GridObjectDelivery[] taskGridObjectDeliveryArray;
    public UIGridObjectDelivery[] taskUiGridObjectDeliveryArray;
    public void SetTask(Colony currentColony)
    {
        taskOrigin = currentColony.colonyName; // Sets the origin of the UI tasks posteriorly created
        taskDestination = currentColony.associatedColonyArray[Random.Range(0, currentColony.associatedColonyArray.Length)]; // Sets the destination of the task based on the amount of associated colonies
        taskContentAmount = Random.Range(currentColony.contentMinAmountPerTask, currentColony.contentMaxAmountPerTask + 1); // Determines the amount of content to be created
        taskGearcoinAmount = 20 * taskContentAmount;
        taskInformationAmount = 10 * taskContentAmount;
        taskGridObjectDeliveryArray = new GridObjectDelivery[taskContentAmount]; // Sets the content array size
        taskUiGridObjectDeliveryArray = new UIGridObjectDelivery[taskContentAmount]; // Sets the UI content array size

        for (int i = 0; i < taskGridObjectDeliveryArray.Length; i++)
        {
            int random = Random.Range(0, UITaskMenuSystem.Instance.allGridObjectDeliveryPrefabArray.Length); // Choose one over all of the grid object deliveries available
            GridObjectDelivery gridObjectDelivery = Instantiate(UITaskMenuSystem.Instance.allGridObjectDeliveryPrefabArray[random], Vector3.zero, Quaternion.identity); // Gets the grid object
            taskDescription += $"{gridObjectDelivery.description}\n";
            taskGridObjectDeliveryArray[i] = gridObjectDelivery; // Stores the grid object on the array

            UIGridObjectDelivery uiGridObjectDelivery = Instantiate(UITaskMenuSystem.Instance.uiDeliveryPrefab, Vector3.zero, Quaternion.identity); // Instantiates the UI of the gridObject
            uiGridObjectDelivery.SetGridObjectDelivery(gridObjectDelivery); // Sets the prefab of the UI grid object
            uiGridObjectDelivery.transform.SetParent(UITaskMenuSystem.Instance.uiDeliveriesContainer); // Sets the transform of the UI grid object
            uiGridObjectDelivery.transform.localScale = Vector3.one; // Resets the scale of the UI grid object
            uiGridObjectDelivery.gameObject.SetActive(false); // Deactivates the UI grid object
            taskUiGridObjectDeliveryArray[i] = uiGridObjectDelivery; // Stores the UI grid object
        }

        task = new Task(taskOrigin, taskDestination, taskContentAmount,
                        taskGearcoinAmount, taskInformationAmount, taskDescription,
                        taskGridObjectDeliveryArray, taskUiGridObjectDeliveryArray);
    }
    public void ActivateDeliveries()
    {
        for (int i = 0; i < taskUiGridObjectDeliveryArray.Length; i++)
        {
            taskUiGridObjectDeliveryArray[i].gameObject.SetActive(true);
        }
        imageState.color = new Color(0.3f, 1f, 0.6f, 0.8f); // Green
        PlayerData.Instance.taskList.Add(this.task);
    }
    public void TryDeactivateDeliveries()
    {
        for (int i = 0; i < taskUiGridObjectDeliveryArray.Length; i++)
        {
            if (taskUiGridObjectDeliveryArray[i].isPlaced == true)
            {
                UIUserInterface.Instance.PopResult("Remove the task's deliveries first!", Color.red); return;
            }
        }
        DeactivateDeliveries();
    }
    public void DeactivateDeliveries()
    {
        for (int i = 0; i < taskUiGridObjectDeliveryArray.Length; i++)
        {
            taskUiGridObjectDeliveryArray[i].gameObject.SetActive(false);
        }
        imageState.color = new Color(1f, 0.4f, 0.5f, 0.8f); // Red
        PlayerData.Instance.taskList.Remove(this.task);
    }
    public void OnClickRequestTaskDescriptionUpdate()
    {
        UITaskMenuSystem.Instance.UpdateTaskDescription(this);
    }
}