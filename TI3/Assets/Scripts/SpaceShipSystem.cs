using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipSystem : MonoBehaviour
{
    public static SpaceShipSystem Instance;
    [Header("All facilities related")]
    public UIGridObjectFacility[] allUiGridObjectFacilityArray;
    public GridObjectFacility[] allGridObjectFacilityPrefabArray;
    public UIFacilityCard[] allUIFacilityCardArray;
    public Worker workerPrefab;
    private void Awake()
    {
        #region ErrorTreatment
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Debug.LogError($" ----- There is more than one UIFacilitiesSystem!!! ----- {this.transform.position} ----- {this.gameObject} -----");
            Destroy(this.gameObject);
            return;
        }
        #endregion
    }
    public void UpdateFacilitiesResearched(bool[] facilitiesResearched)
    {
        for (int i = 0; i < allUIFacilityCardArray.Length; i++)
        {
            if (facilitiesResearched[i] == true) { allUIFacilityCardArray[i].Unlock(); }
        }
    }
    public void UpdateFacilitiesStored(byte[] facilitiesOwned)
    {
        for (int i = 0; i < allUiGridObjectFacilityArray.Length; i++)
        {
            allUiGridObjectFacilityArray[i].UpdateCurrentAmount(facilitiesOwned[i]);
        }
    }
}
