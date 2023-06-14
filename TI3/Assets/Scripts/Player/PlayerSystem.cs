using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    public static PlayerSystem Instance;
    public sbyte currentColonyIndex;
    public string currentColonyName;
    public bool isTravelling;

    public int gearcoins;
    public byte workingEngineer;
    public byte standbyEngineer;
    public int information;

    public void AlterateGearcoins(int value)  { gearcoins += value; UIUserInterface.Instance.UpdateUserInterfaceResources(); }
    public void AlterateInformation(int value) { information += value; UIUserInterface.Instance.UpdateUserInterfaceResources(); }

    public List<Task> taskList; public List<Task> GetMyTasks() { return taskList; }
    public List<GridObject> gridObjectList; public List<GridObject> GetMyGridObjects() { return gridObjectList; }
    public List<Worker> workerList; public List<Worker> GetMyWorkers() { return workerList; }

    public sbyte masterVolumeValue = 0;
    public sbyte environmentVolumeValue = 0;
    public sbyte soundtrackVolumeValue = 0;
    public sbyte sfxVolumeValue = 0;
    public sbyte uiVolumeValue = 0;

    [Tooltip("Corresponds to the SpaceShipSystem's GridObjectFacilities")]
    public byte[] facilitiesStored; public byte[] GetMyFacilitiesStored() { return facilitiesStored; } // The size of the arrays must be the exact same
    public bool[] facilitiesResearched; public bool[] GetMyFacilitiesResearched() { return facilitiesResearched; } // the size of the arrays must be the exact same
    public void ResearchFacility(int index)
    {
        facilitiesResearched[index] = true;
        UpdateFacilitiesResearched();
    }
    public void UpdateFacilitiesResearched()
    {
        SpaceShipSystem.Instance.UpdateFacilitiesResearched(facilitiesResearched);
    }
    public void AlterateFacilitiesStored(int index, byte value)
    {
        facilitiesStored[index] += value;
        UpdateFacilitiesStored();
    }
    public void UpdateFacilitiesStored()
    {
        SpaceShipSystem.Instance.UpdateFacilitiesStored(facilitiesStored);
    }
    public void AddToGridObjectList(GridObject gridObject)
    {
        gridObjectList.Add(gridObject);
        
    }
    public void RemoveFromGridObjectList(GridObject gridObject)
    {
        gridObjectList.Remove(gridObject);
        
    }

    /// <summary>
    /// Sets the current colony based on the index received
    /// </summary>
    /// <param name="colonyIndex"></param>
    public void SetCurrentColony(sbyte colonyIndex)
    { 
        this.currentColonyIndex = colonyIndex; // Changes the index
        this.currentColonyName = ColonySystem.Instance.allColoniesArray[currentColonyIndex].colonyName; // Gets the name from the all colonies array
    }
    public void DecrementTasksTime(int decrement = 1)
    {
        foreach (Task task in taskList)
        {
            task.time -= decrement;

            if (task.time <= 0) // If task time expired...
            {
                task.isLate = true; // Set task to isLate
                for (int i = 0; i < task.gridObjectDeliveryArray.Length; i++) // Foreach task content...
                { Instantiate(Player.Instance.playerFXs.GetVFXDeliveryLate(), task.gridObjectDeliveryArray[i].transform.position, Quaternion.identity); } // Instantiate isLate effect
            }
        }
    }
    public bool AreTaskDeliveriesPlaced()
    {
        foreach (Task task in taskList)
        {
            for (int i = 0; i < task.gridObjectDeliveryArray.Length; i++)
            {
                if (task.gridObjectDeliveryArray[i].isPlaced == false) { return false; }
            }
        }
        return true;
    }
    public void HealEverything(int healShip, int healBuildings)
    {
        Player.Instance.playerIntegrity.HealDamage(healShip);
        foreach (GridObject gridObject in gridObjectList)
        { gridObject.HealDamage(healBuildings); }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
        #region ErrorTreatment
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Debug.LogWarning($" ----- There is more than one PlayerSystem!!! ----- {this.transform.position} ----- {this.gameObject} -----");
            Destroy(this.gameObject);
            return;
        }
        #endregion
    }
    private void Start()
    {
        ColonySystem.Instance.UpdateCurrentColony(currentColonyIndex);
        UIUserInterface.Instance.UpdateUserInterfaceResources();
        SpaceShipSystem.Instance.UpdateFacilitiesStored(facilitiesStored);
        SpaceShipSystem.Instance.UpdateFacilitiesResearched(facilitiesResearched);
    }
    private void OnLevelWasLoaded(int level)
    {
        if (Instance != this) { return; } // WITHOUT THIS THE NEW PLAYERDATA WILL CREATE ONE TASK FROM EARTH TO MARS

        if (level == 1) { Player.Instance.SetTravellingState(true); return; }

        Player.Instance.SetTravellingState(false);
        ColonySystem.Instance.UpdateCurrentColony(currentColonyIndex);
        SpaceShipSystem.Instance.UpdateFacilitiesStored(facilitiesStored);
        SpaceShipSystem.Instance.UpdateFacilitiesResearched(facilitiesResearched);
        UIUserInterface.Instance.UpdateUserInterfaceResources();

        int totalRewardedGearcoin = 0;
        int totalRewardedInformation = 0;

        List<Task> accomplishedTaskList = new List<Task>();

        foreach (Task task in taskList)
        {
            task.time -= 1;

            if (task.time <= 0) // If task time expired...
            { 
                task.isLate = true; // Set task to isLate
                for (int i = 0; i < task.gridObjectDeliveryArray.Length; i++) // Foreach task content...
                { Instantiate(Player.Instance.playerFXs.GetVFXDeliveryLate(), task.gridObjectDeliveryArray[i].transform.position, Quaternion.identity); } // Instantiate isLate effect
            }

            if (task.destination != currentColonyName) { continue; } // If is not the task's destination, continue

            if (task.isLate == false) // If is not late...
            {
                totalRewardedGearcoin += task.gearcoinAmount;
                totalRewardedInformation += task.informationAmount;
            }
            else if (task.isLate == true) // If is late...
            {
                totalRewardedGearcoin += task.gearcoinAmount / 2;
                totalRewardedInformation += task.informationAmount / 2;
            }

            for (int i = 0; i < task.gridObjectDeliveryArray.Length; i++) // Foreach task content...
            {
                if (task.gridObjectDeliveryArray[i] == null) { continue; }
                task.gridObjectDeliveryArray[i].DeliverGridObjectDelivery();
            } // Deliver the content...

            accomplishedTaskList.Add(task); // Adds the task to the accomplished tasks list
            // This is needed because you can't remove a task from a list while it is in a loop
        }

        foreach (Task task in accomplishedTaskList)
        {
            taskList.Remove(task);
        }

        if (totalRewardedGearcoin != 0)
        {
            this.gearcoins += totalRewardedGearcoin;
            UIUserInterface.Instance.gearcoinText.text = gearcoins.ToString();
            UIUserInterface.Instance.PopResult($"+{totalRewardedGearcoin}",
                                               new Color(1f, 0.5f, 0f, 1f), 5,
                                               UIUserInterface.Instance.gearcoinPopUpTransform);
        }
        if (totalRewardedInformation != 0)
        {
            this.information += totalRewardedInformation;
            UIUserInterface.Instance.informationText.text = information.ToString();
            UIUserInterface.Instance.PopResult($"+{totalRewardedInformation}",
                                               new Color(0f, 0.5f, 1f, 1f), 5,
                                               UIUserInterface.Instance.informationPopUpTransform);
        }
    }
}