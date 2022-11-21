using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControllerEvent : MonoBehaviour
{
    [Header("Must be setted")]
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private void Start()
    {
        cinemachineVirtualCamera.Follow = Player.Instance.playerCameraEventFollowTransform;
        cinemachineVirtualCamera.LookAt = Player.Instance.playerCameraEventLookAtTransform;
    }
}
