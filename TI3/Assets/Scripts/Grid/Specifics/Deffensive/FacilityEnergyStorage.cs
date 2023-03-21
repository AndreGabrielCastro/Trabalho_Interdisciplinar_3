using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityEnergyStorage : MonoBehaviour
{
    [Header("Must be setted")]
    [SerializeField] private int extraEnergy;
    private void OnEnable()
    {
        Player.Instance.playerEnergy.AlterateMaximumEnergy(+extraEnergy);
    }
    private void OnDisable()
    {
        Player.Instance.playerEnergy.AlterateMaximumEnergy(-extraEnergy);
    }
}