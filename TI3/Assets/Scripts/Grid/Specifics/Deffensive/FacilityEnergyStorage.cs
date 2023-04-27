using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityEnergyStorage : MonoBehaviour, IFacility
{
    [Header("Must be setted")]
    [SerializeField] private int extraEnergy;
    public void StartWork()
    {
        AlterateMaximumEnergy(+extraEnergy);
    }
    public void StopWork()
    {
        AlterateMaximumEnergy(-extraEnergy);
    }
    private void AlterateMaximumEnergy(int extra)
    {
        Player.Instance.playerEnergy.AlterateMaximumEnergy(extra);
    }
    private void OnEnable()
    {
        AlterateMaximumEnergy(+extraEnergy);
    }
    private void OnDisable()
    {
        AlterateMaximumEnergy(-extraEnergy);
    }
}