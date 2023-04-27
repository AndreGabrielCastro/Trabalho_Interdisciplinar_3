using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityEnergyGenerator : MonoBehaviour, IFacility
{
    [Header("Must be setted")]
    [SerializeField] private float extraEnergyGeneration;
    public void StartWork()
    {
        AlterateEnergyGeneration(+extraEnergyGeneration);
    }
    public void StopWork()
    {
        AlterateEnergyGeneration(-extraEnergyGeneration);
    }
    private void AlterateEnergyGeneration(float extra)
    {
        Player.Instance.playerEnergy.AlterateEnergyGeneration(extra);
    }
    private void OnEnable()
    {
        AlterateEnergyGeneration(+extraEnergyGeneration);
    }
    private void OnDisable()
    {
        AlterateEnergyGeneration(-extraEnergyGeneration);
    }
}