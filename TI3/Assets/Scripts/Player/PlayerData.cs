using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    public int currentColonyIndex;
    public string currentColonyName;

    
    public void SetCurrentColony(int colonyIndex)
    { 
        this.currentColonyIndex = colonyIndex; // Changes the index
        this.currentColonyName = ColonySystem.Instance.allColoniesArray[currentColonyIndex].colonyName; // Gets the name from the all colonies array
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
            Debug.LogError($" ----- There is more than one PlayerData!!! ----- {this.transform.position} ----- {this.gameObject} -----");
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
        if (level == 0) { UpdateCurrentColony(); Debug.Log("Carregou"); }
    }
    public void UpdateCurrentColony() { ColonySystem.Instance.UpdateCurrentColony(currentColonyIndex); }
}
