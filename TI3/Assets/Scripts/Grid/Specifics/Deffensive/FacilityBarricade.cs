using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityBarricade : MonoBehaviour, IFacility
{
    [Header("Must be setted")]
    [SerializeField] private int extraIntegrity;
    public void StartWork()
    {
        AlterateMaximumIntegrity(+extraIntegrity);
    }
    public void StopWork()
    {
        AlterateMaximumIntegrity(-extraIntegrity);
    }
    private void AlterateMaximumIntegrity(int extra)
    {
        Player.Instance.playerIntegrity.AlterateMaximumIntegrity(extra);
    }
    private void OnEnable()
    {
        AlterateMaximumIntegrity(+extraIntegrity);
    }
    private void OnDisable()
    {
        AlterateMaximumIntegrity(-extraIntegrity);
    }
}
