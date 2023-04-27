using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommand : MonoBehaviour
{
    [Header("Setted during playtime")]
    [SerializeField] private List<Worker> selectedWorkers = new List<Worker>();
    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            TrySendWorkers();
        }
    }
    private void TrySendWorkers()
    {
        selectedWorkers = Player.Instance.playerSelection.GetSelectedWorkers();
        if (selectedWorkers.Count <= 0) { return; }

        Vector3 worldPosition = MouseSystem.Instance.GetWorldPosition();
        GridPosition localGridPosition = GridSystem.Instance.GetGridGroundPositionRelative(worldPosition);
        GridTile gridTile = GridSystem.Instance.TryGetGridTile(localGridPosition);
        if (gridTile == null) { return; }
        Vector3 localPosition = GridSystem.Instance.GetWorldPositionWithoutOffset(localGridPosition);

        if (selectedWorkers.Count == 1)
        {
            selectedWorkers[0].TrySetDestination(gridTile, localPosition); return;
        }
        else if (selectedWorkers.Count > 1)
        {
            if (gridTile.gridObject == null)
            {
                selectedWorkers[0].TrySetDestination(gridTile, localPosition);
            }
            else if (gridTile.gridObject != null)
            {
                for (int i = 0; i < gridTile.gridObject.gridTileArray.Length; i++)
                {
                    selectedWorkers[i].TrySetDestination(gridTile.gridObject.gridTileArray[i]);
                }
            }
        }
    }
}