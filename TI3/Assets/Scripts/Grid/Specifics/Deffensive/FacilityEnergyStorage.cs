using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityEnergyStorage : MonoBehaviour, Facility
{
    [Header("Must be setted")]
    [SerializeField] private int extraEnergy;
    public void AddWorker()
    {
        AlterateMaximumEnergy(+extraEnergy);
    }
    public void RemoveWorker()
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