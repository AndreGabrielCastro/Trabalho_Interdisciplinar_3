using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoom : MonoBehaviour
{
    [SerializeField] private float maximumCameraHeight;
    [SerializeField] private float minimumCameraHeight;
    private float originalCameraHeight;
    private float currentCameraHeight;
    private float desiredCameraHeight;
    public void ResetCameraHeight()
    {
        currentCameraHeight = originalCameraHeight;
        desiredCameraHeight = originalCameraHeight;
        Player.Instance.playerCameraTransform.localPosition = Vector3.up * originalCameraHeight;

        Player.Instance.playerCameraPivotTransform.SetParent(PlayerSystem.Instance.transform);
        Player.Instance.playerCameraPivotTransform.position = Vector3.zero;
    }
    public void SetCameraHeight(float value)
    {
        currentCameraHeight = value;
        desiredCameraHeight = value;
        Player.Instance.playerCameraTransform.localPosition = Vector3.up * currentCameraHeight;
    }
    private void Start()
    {
        originalCameraHeight = Player.Instance.playerCameraTransform.localPosition.y;
        currentCameraHeight = originalCameraHeight;
        desiredCameraHeight = currentCameraHeight;
    }
    private void Update()
    {
        if (Player.Instance.isTravelling == false) { return; }
        if (Player.Instance.isGameOver == true) { return; }

        if (Input.mouseScrollDelta.y > 0 && desiredCameraHeight > minimumCameraHeight) // Get Close
        {
            if (desiredCameraHeight == minimumCameraHeight) { return; }
            else if (desiredCameraHeight < maximumCameraHeight * 0.5f && Player.Instance.playerCameraTransform.parent != Player.Instance.transform)
            {
                Player.Instance.playerCameraPivotTransform.SetParent(Player.Instance.transform);
            }
            else if (desiredCameraHeight < minimumCameraHeight) { desiredCameraHeight = minimumCameraHeight; }
            desiredCameraHeight -= Time.deltaTime * 50;
        }
        else if (Input.mouseScrollDelta.y < 0 && desiredCameraHeight < maximumCameraHeight) // Get Far
        {
            if (desiredCameraHeight == maximumCameraHeight) { return; }
            else if (desiredCameraHeight > maximumCameraHeight * 0.5f && Player.Instance.playerCameraTransform.parent != PlayerSystem.Instance.transform)
            {
                Player.Instance.playerCameraPivotTransform.SetParent(PlayerSystem.Instance.transform);
            }
            else if (desiredCameraHeight > maximumCameraHeight) { desiredCameraHeight = maximumCameraHeight; }
            desiredCameraHeight += Time.deltaTime * 50;
        }
    }
    private void LerpPivotCameraTransform()
    {
        if (Player.Instance.playerCameraPivotTransform == Player.Instance.playerCameraPivotTransform.parent.transform) { return; }

        Player.Instance.playerCameraPivotTransform.position = Vector3.Lerp(Player.Instance.playerCameraPivotTransform.position,
                                                                           Player.Instance.playerCameraPivotTransform.parent.position,
                                                                           Time.fixedDeltaTime * 5);

        if ((Player.Instance.playerCameraPivotTransform.position - Player.Instance.playerCameraPivotTransform.parent.position).magnitude < 0.01f)
        {
            Player.Instance.playerCameraPivotTransform.position = Player.Instance.playerCameraPivotTransform.parent.position;
        }
    }
    private void LerpMainCameraTransform()
    {
        if (currentCameraHeight == desiredCameraHeight) { return; }

        currentCameraHeight = Mathf.Lerp(currentCameraHeight, desiredCameraHeight, Time.fixedDeltaTime);

        if (Mathf.Abs(currentCameraHeight - desiredCameraHeight) < 0.01f)
        {
            currentCameraHeight = desiredCameraHeight;
        }

        Player.Instance.playerCameraTransform.localPosition = Vector3.up * currentCameraHeight;
    }
    private void FixedUpdate()
    {
        if (Player.Instance.isTravelling == false) { return; }
        if (Player.Instance.isGameOver == true) { return; }

        LerpPivotCameraTransform();
        LerpMainCameraTransform();
    }
}