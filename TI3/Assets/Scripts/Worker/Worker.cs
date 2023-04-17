using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    [SerializeField] private GridTile gridTileDestination; public GridTile GetGridDestination() { return gridTileDestination; }
    [SerializeField] private Vector3 localGridDestination = Vector3.negativeInfinity; public Vector3 GetDestinationPosition() { return localGridDestination; }
    private SpriteRenderer selectedVisual;
    private bool isWorking; public bool GetWorkingState() { return isWorking; }
    private bool isMoving;
    public void BeSelected()
    {
        selectedVisual.enabled = true;
    }
    public void BeDeselected()
    {
        selectedVisual.enabled = false;
    }
    public void TrySetDestination(GridTile gridTile, Vector3 localPosition)
    {
        if (gridTile == gridTileDestination || localPosition == localGridDestination) { return; }
        TryStopWork();
        gridTileDestination = gridTile;
        localGridDestination = localPosition;
    }
    public void TrySetDestination(GridTile gridTile)
    {
        if (gridTile == gridTileDestination) { return; }
        TryStopWork();
        gridTileDestination = gridTile;
        localGridDestination = Player.Instance.transform.InverseTransformPoint(gridTile.transform.position);
    }
    public void TrySetDestination(Vector3 localPosition)
    {
        if (localPosition == localGridDestination) { return; }
        GridPosition localGridPosition = GridSystem.Instance.GetGridGroundPositionRelative(localPosition);
        GridTile gridTile = GridSystem.Instance.TryGetGridTile(localGridPosition);
        if (gridTile == null) { return; }
        Vector3 localPos = GridSystem.Instance.GetWorldPositionWithoutOffset(localGridPosition);
        TryStopWork();
        gridTileDestination = gridTile;
        localGridDestination = localPos;
    }
    public void TryStartWork()
    {
        isMoving = false;
        if (isWorking == true) { return; }
        if (gridTileDestination.worker != null && gridTileDestination.worker != this) { return; }
        if (gridTileDestination.gridObject == null) { return; }
        if (gridTileDestination.gridObject.TryGetComponent<GridObjectFacility>(out GridObjectFacility gridObjectFacility) == true)
        {
            isWorking = true;
            gridTileDestination.SetWorker(this);
            gridObjectFacility.TryAddWorker();
        }
    }
    public void TryStopWork()
    {
        isMoving = true;
        if (isWorking == false) { return; }
        if (gridTileDestination == null) { return; }
        if (gridTileDestination.worker != null && gridTileDestination.worker != this) { return; }
        if (gridTileDestination.gridObject == null) { return; }
        if (gridTileDestination.gridObject.TryGetComponent<GridObjectFacility>(out GridObjectFacility gridObjectFacility) == true)
        {
            isWorking = false;
            gridTileDestination.SetWorker(null);
            gridObjectFacility.TryRemoveWorker();
        }
    }
    public void TryMove()
    {
        if (isMoving == false) { return; }
        Vector3 distance = (localGridDestination - transform.localPosition);
        if (distance.magnitude <= 0.01f) { transform.localPosition = localGridDestination; TryStartWork(); }
        transform.localPosition = Vector3.Lerp(transform.localPosition, localGridDestination, 2 * Time.fixedDeltaTime);
        Vector3 direction = distance.normalized;
        transform.forward = Vector3.Lerp(transform.forward, direction, 4 * Time.fixedDeltaTime);
    }
    private void Awake()
    {
        selectedVisual = GetComponentInChildren<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        TryMove();
    }
}