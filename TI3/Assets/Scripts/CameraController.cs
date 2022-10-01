using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineTransposer cinemachineTransposer;
    private Vector3 targetFollowOffset;
    private float maxFollowOffsetY = 11.5f;
    private float maxFollowOffsetZ = -5f;
    private float minFollowOffsetY = 1.5f;
    private float minFollowOffsetZ = -10f;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] int moveSpeed = 10;
    [SerializeField] int rotateSpeed = 60;
    [SerializeField] int zoomSpeed = 1;
    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }
    private void HandleMovement()
    {
        Vector3 moveDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) { moveDirection.z += 1; }
        if (Input.GetKey(KeyCode.A)) { moveDirection.x -= 1; }
        if (Input.GetKey(KeyCode.S)) { moveDirection.z -= 1; }
        if (Input.GetKey(KeyCode.D)) { moveDirection.x += 1; }
        Vector3 moveVector = transform.forward * moveDirection.z + transform.right * moveDirection.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }
    private void HandleRotation()
    {
        Vector3 rotateDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q)) { rotateDirection.y -= 1; }
        if (Input.GetKey(KeyCode.E)) { rotateDirection.y += 1; }
        transform.eulerAngles += rotateDirection * rotateSpeed * Time.deltaTime;
    }
    private void HandleZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= 1;
            //targetFollowOffset.y -= 1 * ((targetFollowOffset.y - 1.5f) * 0.1f);
            targetFollowOffset.z += 0.5f;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += 1;
            //targetFollowOffset.y += 1 * ((targetFollowOffset.y - 1.5f) * 0.1f);
            targetFollowOffset.z -= 0.5f;
        }
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, minFollowOffsetY, maxFollowOffsetY);
        targetFollowOffset.z = Mathf.Clamp(targetFollowOffset.z, minFollowOffsetZ, maxFollowOffsetZ);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }
}