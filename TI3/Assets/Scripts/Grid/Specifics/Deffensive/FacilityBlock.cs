using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityBlock : MonoBehaviour
{
    [Header("Must be setted")]
    [SerializeField] private int extraIntegrity;
    private void OnEnable()
    {
        Player.Instance.playerIntegrity.AlterateMaximumIntegrity(+extraIntegrity);
    }
    private void OnDisable()
    {
        Player.Instance.playerIntegrity.AlterateMaximumIntegrity(-extraIntegrity);
    }
}
