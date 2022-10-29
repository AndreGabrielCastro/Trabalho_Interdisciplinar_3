using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    public int currentColonyIndex;
    public string currentColonyName;

    public int gearcoins;
    public int workingEngineer;
    public int standbyEngineer;
    public int information;

    public List<Task> taskList;

    /// <summary>
    /// Sets the current colony based on the index received
    /// </summary>
    /// <param name="colonyIndex"></param>
    public void SetCurrentColony(int colonyIndex)
    { 
        this.currentColonyIndex = colonyIndex; // Changes the index
        this.currentColonyName = ColonySystem.Instance.allColoniesArray[currentColonyIndex].colonyName; // Gets the name from the all colonies array
    }
    public void UpdateCurrentColony() { ColonySystem.Instance.UpdateCurrentColony(currentColonyIndex); }
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
            Debug.LogWarning($" ----- There is more than one PlayerData!!! ----- {this.transform.position} ----- {this.gameObject} -----");
            Destroy(this.gameObject);
            return;
        }
        #endregion
    }
    private void Start()
    {
        UpdateCurrentColony();
    }
    private void OnLevelWasLoaded(int level)
    {
        if (Instance != this) { return; } // WITHOUT THIS THE NEW PLAYERDATA WILL CREATE ONE TASK FROM EARTH TO MARS

        if (level != 0) { return; }

        UpdateCurrentColony();

        int totalRewardedGearcoin = 0;
        int totalRewardedInformation = 0;

        List<Task> accomplishedTaskList = new List<Task>();

        foreach (Task task in taskList)
        {
            if (task.destination != currentColonyName) { continue; }

            totalRewardedGearcoin += task.gearcoinAmount;
            totalRewardedInformation += task.informationAmount;

            for (int i = 0; i < task.gridObjectDeliveryArray.Length; i++)
            { task.gridObjectDeliveryArray[i].DeliverGridObjectDelivery(); }

            accomplishedTaskList.Add(task);
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
