using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectFacility : GridObject
{
    [Header("Setted during playtime")]
    public UIGridObjectFacility uiGridObjectFacility;
    public void AddWorker()
    {
        GetComponent<Facility>().AddWorker();
    }
    public void RemoveWorker()
    {
        GetComponent<Facility>().RemoveWorker();
    }
    private void RemoveAllWorkers()
    {
        foreach (GridTile gridtile in gridTileArray)
        {
            if (gridtile.worker != null)
            {
                gridtile.worker.Reset();
                GetComponent<Facility>().RemoveWorker();
            }
        }
    }
    public void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        { uiGridObjectFacility = SpaceShipSystem.Instance.allUiGridObjectFacilityArray[prefabIndex]; }
    }
    public void DeleteGridObjectFacility()
    {
        RemoveAllWorkers();
        foreach (GridTile gridTile in gridTileArray) // Foreach grid tile it occupies...
        { gridTile.SetGridObject(null); } // Set the grid tile to null
        uiGridObjectFacility.UpdateCurrentAmount(+1); // Increases +1 to the UI grid object amount
        Instantiate(VfxSystem.Instance.vfxDeleted, this.transform.position + Vector3.up * 0.1f, Quaternion.identity);
        PlayerSystem.Instance.gridObjectList.Remove(this);
        Destroy(this.gameObject); // Destroy the object
    }
    public void DestroyGridObjectFacility()
    {
        RemoveAllWorkers();
        foreach (GridTile gridTile in gridTileArray) // Foreach grid tile it occupies...
        { gridTile.SetGridObject(null); } // Set the grid tile to null
        Instantiate(VfxSystem.Instance.vfxDestroyed, this.transform.position + Vector3.up * 0.1f, Quaternion.identity);
        PlayerSystem.Instance.gridObjectList.Remove(this);
        Destroy(this.gameObject); // Destroy the object
    }
}
