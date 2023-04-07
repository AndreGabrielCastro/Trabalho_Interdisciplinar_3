using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    [SerializeField] private GridTile gridTileDestination; public GridTile GetGridDestination() { return gridTileDestination; }
    [SerializeField] private Vector3 localGridDestination = Vector3.negativeInfinity; public Vector3 GetDestinationPosition() { return localGridDestination; }
    private bool isMoving;
    private bool isWorking;
    public void SetDestination(GridTile gridTile, Vector3 localPosition)
    {
        if (gridTile == gridTileDestination || localPosition == localGridDestination) { return; }
        TryStopWork();
        gridTileDestination = gridTile;
        localGridDestination = localPosition;
    }
    public void SetDestination(GridTile gridTile)
    {
        if (gridTile == gridTileDestination) { return; }
        TryStopWork();
        gridTileDestination = gridTile;
        localGridDestination = Player.Instance.transform.InverseTransformPoint(gridTile.transform.position);
    }
    public void TryStartWork()
    {
        isMoving = false;
        if (isWorking == true) { return; }
        if (gridTileDestination.gridObject == null) { return; }
        if (gridTileDestination.gridObject.TryGetComponent<GridObjectFacility>(out GridObjectFacility gridObjectFacility) == true)
        {
            isWorking = true;
            gridObjectFacility.TryAddWorker();
        }
    }
    public void TryStopWork()
    {
        isMoving = true;
        if (isWorking == false) { return; }
        if (gridTileDestination == null) { return; }
        if (gridTileDestination.gridObject == null) { return; }
        if (gridTileDestination.gridObject.TryGetComponent<GridObjectFacility>(out GridObjectFacility gridObjectFacility) == true)
        {
            isWorking = false;
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
        transform.forward = Vector3.Lerp(transform.forward, direction, 2 * Time.fixedDeltaTime);
    }
    private void FixedUpdate()
    {
        TryMove();
    }
}