using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityEnergyGenerator : MonoBehaviour
{
    [Header("Must be setted")]
    [SerializeField] private float extraEnergyGeneration;
    private void OnEnable()
    {
        Player.Instance.playerEnergy.AlterateEnergyGeneration(+extraEnergyGeneration);
    }
    private void OnDisable()
    {
        Player.Instance.playerEnergy.AlterateEnergyGeneration(-extraEnergyGeneration);
    }
}