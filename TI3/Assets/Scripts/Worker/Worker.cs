using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    [Header("Setted during playtime")]
    [SerializeField] private SpriteRenderer selectedVisual;
    [SerializeField] private GridTile gridTileDestination; 
    [SerializeField] private Vector3 localGridDestination = Vector3.negativeInfinity; 
    [SerializeField] private bool isWorking; 
    [SerializeField] private bool isMoving;

    #region Getters&Setters
    public GridTile GetGridDestination() { return gridTileDestination; }
    public Vector3 GetDestinationPosition() { return localGridDestination; }
    public bool GetWorkingState() { return isWorking; }
    private void SetDestination(GridTile gridTile, Vector3 localPosition)
    {
        if (gridTileDestination != null)
        {
            gridTileDestination.SetWorker(null);
        }
        gridTileDestination = gridTile;
        localGridDestination = localPosition;
        gridTileDestination.SetWorker(this);
    }
    #endregion
    public void BeSelected()
    {
        selectedVisual.enabled = true;
    }
    public void BeDeselected()
    {
        selectedVisual.enabled = false;
    }
    public void Reset()
    {
        gridTileDestination = null;
        localGridDestination = Vector3.negativeInfinity;
        isWorking = false;
        isMoving = false;
    }
    public void TrySetDestination(GridTile gridTile, Vector3 localPosition)
    {
        if (gridTile == gridTileDestination || localPosition == localGridDestination) { return; }
        if (gridTile.worker != null && gridTile.worker != this) { return; }
        TryStopWork();
        SetDestination(gridTile, localPosition);
    }
    public void TrySetDestination(GridTile gridTile)
    {
        if (gridTile == gridTileDestination) { return; }
        if (gridTile.worker != null && gridTile.worker != this) { return; }
        TryStopWork();
        SetDestination(gridTile, Player.Instance.transform.InverseTransformPoint(gridTile.transform.position));
    }
    public void TrySetDestination(Vector3 localPosition)
    {
        if (localPosition == localGridDestination) { return; }
        GridPosition localGridPosition = GridSystem.Instance.GetGridGroundPositionRelative(localPosition);
        GridTile gridTile = GridSystem.Instance.TryGetGridTile(localGridPosition);
        if (gridTile == null) { return; }
        if (gridTile.worker != null && gridTile.worker != this) { return; }
        Vector3 localPos = GridSystem.Instance.GetWorldPositionWithoutOffset(localGridPosition);
        TryStopWork();
        SetDestination(gridTile, localPos);
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
            gridObjectFacility.StartWork();
        }
    }
    public void TryStopWork()
    {
        isMoving = true;
        if (isWorking == false) { return; }
        if (gridTileDestination == null) { return; }
        if (gridTileDestination.worker != null && gridTileDestination.worker != this) { return; }
        if (gridTileDestination.gridObject == null)
        {
            isWorking = false;
            gridTileDestination.SetWorker(null);
        }
        else if (gridTileDestination.gridObject.TryGetComponent<GridObjectFacility>(out GridObjectFacility gridObjectFacility) == true)
        {
            isWorking = false;
            gridTileDestination.SetWorker(null);
            gridObjectFacility.StopWork();
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