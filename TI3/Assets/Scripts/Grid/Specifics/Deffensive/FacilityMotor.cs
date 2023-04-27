using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityMotor : MonoBehaviour, IFacility
{
    [Header("Must be setted")]
    [SerializeField] private float extraSpeed;
    public void StartWork()
    {
        AlterateSpeed(+extraSpeed);
    }
    public void StopWork()
    {
        AlterateSpeed(-extraSpeed);
    }
    private void AlterateSpeed(float extra)
    {
        Player.Instance.playerMovement.AlterateSpeed(extra);
    }
    private void OnEnable()
    {
        AlterateSpeed(+extraSpeed);
    }
    private void OnDisable()
    {
        AlterateSpeed(-extraSpeed);
    }
}