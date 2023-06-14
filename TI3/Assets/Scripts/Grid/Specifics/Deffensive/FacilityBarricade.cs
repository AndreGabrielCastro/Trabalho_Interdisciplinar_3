using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityBarricade : MonoBehaviour, IFacility
{
    [Header("Must be setted")]
    [SerializeField] private int extraIntegrity;
    public void StartWork()
    {
        AlterateMaximumAndCurrentIntegrity(+extraIntegrity);
    }
    public void StopWork()
    {
        AlterateMaximumAndCurrentIntegrity(-extraIntegrity);
    }
    private void AlterateMaximumAndCurrentIntegrity(int extra)
    {
        Player.Instance.playerIntegrity.AlterateMaximumAndCurrentIntegrity(extra);
    }
    private void OnEnable()
    {
        AlterateMaximumAndCurrentIntegrity(+extraIntegrity);
    }
    private void OnDisable()
    {
        AlterateMaximumAndCurrentIntegrity(-extraIntegrity);
    }
}
