using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITask : MonoBehaviour
{
    public string origin;
    public string destination;

    public Image imageState;

    public GridObject[] gridObjectContentArray;
    public UIGridObject[] uiGridObjectContentArray;
    public void SetTask(Colony currentColony)
    {
        origin = currentColony.colonyName; // Sets the origin of the tasks posteriorly created
        int random = Random.Range(0, currentColony.associatedColonyArray.Length); // Gets the associated colonies of the current colony
        destination = currentColony.associatedColonyArray[random]; // Sets the destination of the task based on the amount of associated colonies
        random = Random.Range(currentColony.contentMinAmountPerTask, currentColony.contentMaxAmountPerTask + 1); // Determines the amount of tasks to be created
        gridObjectContentArray = new GridObject[random]; // Sets the array size
        uiGridObjectContentArray = new UIGridObject[random]; // Sets the array size

        for (int i = 0; i < gridObjectContentArray.Length; i++)
        {
            random = Random.Range(0, ColonySystem.Instance.gridObjectDeliveryArray.Length); // Choose one over all the gridObjectDeliveries available
            GridObject gridObject = ColonySystem.Instance.gridObjectDeliveryArray[random]; // Gets the gridObject
            gridObjectContentArray[i] = gridObject; // Stores the gridObject on the array

            UIGridObject uiGridObject = Instantiate(UITaskMenuSystem.Instance.uiDeliveryPrefab, Vector3.zero, Quaternion.identity); // Instantiates the UI of the gridObject
            uiGridObject.gridObjectPrefab = gridObjectContentArray[i].gameObject; // Sets the prefab of the UIGridObject
            uiGridObject.transform.SetParent(UITaskMenuSystem.Instance.uiDeliveriesContainer); // Sets the transform of the UIGridObject
            uiGridObject.transform.localScale = Vector3.one; // Resets the localScale of the UIGridObject
            uiGridObject.gameObject.SetActive(false); // Deactivates the UIGridObject
            uiGridObjectContentArray[i] = uiGridObject; // Stores the UIGridObject
        }
    }
    public void ActivateDeliveries()
    {
        for (int i = 0; i < uiGridObjectContentArray.Length; i++)
        {
            uiGridObjectContentArray[i].gameObject.SetActive(true);
        }
        imageState.color = new Color(0.3f, 1f, 0.6f, 0.8f);
    }
    public void TryDeactivateDeliveries()
    {
        for (int i = 0; i < uiGridObjectContentArray.Length; i++)
        {
            if (uiGridObjectContentArray[i].curAmount != uiGridObjectContentArray[i].maxAmount)
            {
                UITaskMenuSystem.Instance.PopResult(false); return;
            }
        }
        DeactivateDeliveries();
    }
    public void DeactivateDeliveries()
    {
        for (int i = 0; i < uiGridObjectContentArray.Length; i++)
        {
            uiGridObjectContentArray[i].gameObject.SetActive(false);
        }
        imageState.color = new Color(1f, 0.4f, 0.5f, 0.8f);
        UITaskMenuSystem.Instance.PopResult(true); return;
    }
    public void OnClickRequestTaskDescriptionUpdate()
    {
        UITaskMenuSystem.Instance.UpdateTaskDescription(this);
    }
}