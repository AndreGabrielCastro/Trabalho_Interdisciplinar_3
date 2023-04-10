using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipSystem : MonoBehaviour
{
    public static SpaceShipSystem Instance;
    [Header("All facilities related")]
    public UIGridObjectFacility[] allUiGridObjectFacilityArray;
    public GridObjectFacility[] allGridObjectFacilityPrefabArray;
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
}
