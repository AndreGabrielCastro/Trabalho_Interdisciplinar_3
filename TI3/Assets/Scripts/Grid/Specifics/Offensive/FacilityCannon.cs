using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityCannon : MonoBehaviour, IFacility
{
    private FacilityOffensive facilityOffensive;
    [Header("Must be setted")]
    [SerializeField] private float costReduction;
    [SerializeField] private float cooldownReduction;
    private void Awake()
    {
        facilityOffensive = GetComponent<FacilityOffensive>();
    }
    public void StartWork()
    {
        facilityOffensive.AlterateShootCost(-costReduction);
        facilityOffensive.AlterateReloadTime(-cooldownReduction);
    }
    public void StopWork()
    {
        facilityOffensive.AlterateShootCost(+costReduction);
        facilityOffensive.AlterateReloadTime(+cooldownReduction);
    }
}
