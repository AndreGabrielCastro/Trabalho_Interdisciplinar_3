using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityMotor : MonoBehaviour
{
    [Header("Must be setted")]
    [SerializeField] private float extraSpeed;
    private void OnEnable()
    {
        Player.Instance.playerMovement.AlterateSpeed(+extraSpeed);
    }
    private void OnDisable()
    {
        Player.Instance.playerMovement.AlterateSpeed(-extraSpeed);
    }
}