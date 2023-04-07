using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityBlock : MonoBehaviour, Facility
{
    [Header("Must be setted")]
    [SerializeField] private int extraIntegrity;
    public void AddWorker()
    {
        AlterateMaximumIntegrity(+extraIntegrity);
    }
    public void RemoveWorker()
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
