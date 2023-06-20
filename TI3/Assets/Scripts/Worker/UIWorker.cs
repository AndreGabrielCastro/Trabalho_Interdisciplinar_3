using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIWorker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Worker workerPrefab;
    [SerializeField] private TMP_Text currentAmountText;
    [SerializeField] private int currentAmount = 1;

    [SerializeField] private GameObject workerPreview;
    [SerializeField] private GameObject workerPreviewInstance;
    [SerializeField] private MeshRenderer workerPreviewInstanceRenderer;

    private Vector3 desiredWorldPosition;
    private Quaternion desiredWorldRotation;

    private bool isDragging;

    public void UpdateCurrentAmount(int value)
    {
        currentAmount = value;
        currentAmountText.text = currentAmount.ToString();
    }
    public void AlterateCurrentAmount(int valueToIncreaseOrDecrease)
    {
        PlayerSystem.Instance.AlterateWorkersStored(valueToIncreaseOrDecrease);
    }

    private void Awake()
    {
        workerPreview = workerPrefab.transform.Find("Mesh").gameObject; // Gets the grid object mesh game object and stores it
    }
    private void FixedUpdate()
    {
        if (isDragging == false) { return; } // Avoid the code from running when not dragging any object
        workerPreviewInstance.transform.position = Vector3.Lerp(workerPreviewInstance.transform.position, desiredWorldPosition, Time.deltaTime * 10); // Moves object preview to the desired position smoothly
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (pointerEventData.button != PointerEventData.InputButton.Left) { return; } // If the click isn't from left mouse button, returns

        if (currentAmount == 0) { return; }
        else if (currentAmount < 0) { Debug.LogWarning("Current amount below zero, something is wrong!"); return; }

        // VERIFICAR A DISPONIBILIDADE DE RECURSO AQUI

        // This body of code avoids the object from being created in the sky.
        Vector3 worldPosition = MouseSystem.Instance.GetWorldPosition(); // Gets the world position of the mouse
        GridPosition gridPosition = GridSystem.Instance.GetGridGroundPosition(worldPosition); // Gets the grid position from the world position
        worldPosition = GridSystem.Instance.GetWorldPositionWithoutOffset(gridPosition); // Rounds the world position to grid position

        // This body of code sets important variables
        desiredWorldPosition = worldPosition; // Without this there will be a "glitch" every click. The object will "flick" it's position from nowhere.
        desiredWorldRotation = Quaternion.identity; // Resets the desired rotation
        isDragging = true; // Activates the smooth in FixedUpdate() and enables rotations in the Update();

        // This body of code creates and instantiates the preview and scales it a little bigger to avoid Z Fight
        workerPreviewInstance = Instantiate(workerPreview, worldPosition, Quaternion.identity); // Creates the object at the mouse position and stores it
        workerPreviewInstance.transform.localScale = workerPreviewInstance.transform.localScale * 1.01f; // Scales it a little bigger to avoid Z fight
        workerPreviewInstanceRenderer = workerPreviewInstance.GetComponentInChildren<MeshRenderer>(); // REMOVER INCHILDREN FUTURAMENTE
        Debug.Log("Sometimes the object may be fully scaled to 1. if this happens, change and change back the scale of the object in the prefab");
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (pointerEventData.button != PointerEventData.InputButton.Left) { return; }

        if (currentAmount == 0) { return; }
        else if (currentAmount < 0) { Debug.LogWarning("Current amount below zero, something is wrong!"); return; }

        // This body of code returns the variables needed to the verification right below. It's maximum optimized, don't worry.
        Vector3 worldPosition = MouseSystem.Instance.GetWorldPosition(); // Gets the world position of the mouse
        GridPosition gridPosition = GridSystem.Instance.GetGridGroundPosition(worldPosition);

        worldPosition = GridSystem.Instance.GetWorldPositionWithoutOffset(gridPosition); // Gets the world position from the grid edge position
        desiredWorldPosition = worldPosition; // Sets the desired world position. This is necessary to smoothly change the position in FixedUpdate()

        GridTile gridTile = GridSystem.Instance.TryGetGridTile(gridPosition); // Try to get the grid tile from the grid position

        if (gridTile == null) { workerPreviewInstanceRenderer.material.SetColor("_BaseColor", Color.black); return; } // Turns the renderer to black and return;

        if (gridTile.gridObject == null) // if the grid tile is available...
        {
            Color whiteColor = new Color(0.7f, 0.7f, 0.7f, 1);
            workerPreviewInstanceRenderer.material.SetColor("_BaseColor", whiteColor); // Turns the renderer color to white
        }
        else if (gridTile.gridObject != null) // if the grid tile is occupied...
        {
            Color blackColor = new Color(0.3f, 0.3f, 0.3f, 1);
            workerPreviewInstanceRenderer.material.SetColor("_BaseColor", blackColor); // Turns the renderer color to black
            return;
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (pointerEventData.button != PointerEventData.InputButton.Left) { return; }

        if (currentAmount == 0) { return; }
        else if (currentAmount < 0) { Debug.LogWarning("Current amount below zero, something is wrong!"); return; }

        // This body of code deactivate some functions
        isDragging = false; // Deactivates the smooth in the FixedUpdate() and disables rotations in Update();
        Destroy(workerPreviewInstance); // Get rid of the preview
        Quaternion worldRotation = desiredWorldRotation; // Gets the world rotation before reset

        // This body of code returns the variables needed to the verification right below. It's maximum optimized, don't worry.
        Vector3 worldPosition = MouseSystem.Instance.GetWorldPosition(); // Gets the world position of the mouse
        GridPosition gridPosition = GridSystem.Instance.GetGridGroundPosition(worldPosition);

        worldPosition = GridSystem.Instance.GetWorldPositionWithoutOffset(gridPosition); // Gets the world position from the grid edge position
        desiredWorldPosition = worldPosition; // Sets the desired world position. This is necessary to smoothly change the position in FixedUpdate()

        GridTile gridTile = GridSystem.Instance.TryGetGridTile(gridPosition); // Try to get the grid tile from the grid position considering line and column
        if (gridTile == null) { return; } // If the grid tile does not exist, return;
        if (gridTile.worker != null) { Debug.Log("Can't place over another worker!"); return; } // If the grid tile is occupied, debug and return

        Worker newWorker = Instantiate(workerPrefab, worldPosition, worldRotation); // Creates and instance of the grid object
        newWorker.transform.SetParent(GridSystem.Instance.transform); // Sets this object as the parent of the GridObject

        GridTile newGridTile = GridSystem.Instance.TryGetGridTile(gridPosition); // Try to get the grid tile from the grid position considering line and column
        newGridTile.SetWorker(newWorker); // Sets the grid object to the grid tile and updates the grid visual

        AlterateCurrentAmount(-1);
        Instantiate(Player.Instance.playerFXs.GetVFXGridObjectInstantiated(), newWorker.transform.position + Vector3.up * 0.1f, Quaternion.identity);
        Player.Instance.playerAudio.PlaySong(Player.Instance.playerFXs.GetSFXGridObjectInstantiated());
        PlayerSystem.Instance.AddToWorkerList(newWorker);
    }
}
