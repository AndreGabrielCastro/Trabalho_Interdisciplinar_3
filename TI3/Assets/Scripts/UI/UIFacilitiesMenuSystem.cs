using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFacilitiesMenuSystem : MonoBehaviour
{
    public static UIFacilitiesMenuSystem Instance;
    [Header("All facilities related")]
    public UIGridObjectFacility[] allUiGridObjectFacilityArray;
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
