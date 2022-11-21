using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControllerColony : MonoBehaviour
{
    private Vector3 targetTransformPosition;
    private Vector3 targetOffset;
    private bool isLerping = false;

    private CinemachineTransposer cinemachineTransposer;
    private Vector3 targetFollowOffset;
    private float maxFollowOffsetY = 11.5f;
    private float maxFollowOffsetZ = -5f;
    private float minFollowOffsetY = 1.5f;
    private float minFollowOffsetZ = -10f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private int moveSpeed = 10;
    [SerializeField] private int rotateSpeed = 60;
    [SerializeField] private int zoomSpeed = 1;
    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }
    private void Update()
    {
        HandleMovement();
        HandleRotation();
        //HandleZoom();
    }
    private void FixedUpdate()
    {
        if (isLerping == false) { return; }
        float difference = (targetTransformPosition - this.transform.position).magnitude;
        if (difference > 0.01f)
        { 
            this.transform.position = Vector3.Lerp(this.transform.position, targetTransformPosition, 5 * Time.fixedDeltaTime);
            this.cinemachineTransposer.m_FollowOffset = Vector3.Lerp(this.cinemachineTransposer.m_FollowOffset, targetOffset, 5 * Time.fixedDeltaTime);
        }
        else if (difference <= 0.01f)
        { 
            isLerping = false;
            this.transform.position = targetTransformPosition;
            this.cinemachineTransposer.m_FollowOffset = targetOffset;
        }
    }

    /// <summary>
    /// Lerps the Camera Controller to the Colony position and adjusts the Cinemachine Transposer offset.
    /// </summary>
    /// <param name="colonyTransform"></param>
    public void OnButtonLerpToColony(Transform colonyTransform)
    {
        targetOffset = new Vector3(0, 50, -100);
        targetTransformPosition = colonyTransform.position;
        isLerping = true;
    }

    /// <summary>
    /// Lerps the Camera Controller to the Space Ship position and adjusts the Cinemachine Transposer offset.
    /// </summary>
    public void OnButtonLerpToSpaceShip()
    {
        targetOffset = new Vector3(0, 12, -4);
        targetTransformPosition = Vector3.zero;
        isLerping = true;
    }

    /// <summary>
    /// Lerps the Camera Controller to the Space Ship position and adjusts the Cinemachine Transposer offset.
    /// </summary>
    /// <param name="upgradesTransform"></param>
    public void OnButtonLerpToUpgrades(Transform upgradesTransform)
    {
        targetOffset = new Vector3(0, 4, -4);
        targetTransformPosition = upgradesTransform.position;
        isLerping = true;
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