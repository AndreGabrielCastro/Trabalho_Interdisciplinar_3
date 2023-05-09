using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridObjectDelivery : GridObject
{
    [Header("Setted during playtime")]
    public UIGridObjectDelivery uiGridObjectDelivery;
    public bool isPlaced;

    [Header("Must be defined")]
    public string description = "I should describe this delivery";

    public void DeleteGridObjectDelivery()
    {
        foreach (GridTile gridTile in gridTileArray) // Foreach grid tile it occupies...
        { gridTile.SetGridObject(null); } // Sets the grid tile to null
        if (uiGridObjectDelivery == null)
        {
            UIGridObjectDelivery uiGridObjectDelivery = Instantiate(UITaskMenuSystem.Instance.uiDeliveryPrefab, Vector3.zero, Quaternion.identity); // Instantiates the UI of the gridObject
            uiGridObjectDelivery.SetGridObjectDelivery(this);
            uiGridObjectDelivery.transform.SetParent(UITaskMenuSystem.Instance.uiDeliveriesContainer);
            uiGridObjectDelivery.transform.localScale = Vector3.one;
        }
        else if (uiGridObjectDelivery != null)
        {
            uiGridObjectDelivery.gameObject.SetActive(true);
            uiGridObjectDelivery.isPlaced = false;
            this.gameObject.SetActive(false);
        }
        Instantiate(VfxSystem.Instance.vfxDeleted, this.transform.position + Vector3.up * 0.1f, Quaternion.identity);
        this.isPlaced = false;
        PlayerSystem.Instance.RemoveFromGridObjectList(this);
    }
    public void DestroyGridObjectDelivery()
    {
        foreach (GridTile gridTile in gridTileArray) // Foreach grid tile it occupies...
        { gridTile.SetGridObject(null); } // Set the grid tile to null
        if (uiGridObjectDelivery != null) { Destroy(uiGridObjectDelivery); }
        Instantiate(VfxSystem.Instance.vfxDestroyed, this.transform.position + Vector3.up * 0.1f, Quaternion.identity);
        PlayerSystem.Instance.gridObjectList.Remove(this);
        Destroy(this.gameObject);
    }
    public void DeliverGridObjectDelivery()
    {
        foreach (GridTile gridTile in gridTileArray) // Foreach grid tile it occupies...
        { gridTile.SetGridObject(null); } // Set the grid tile to null
        if (uiGridObjectDelivery != null) { Destroy(uiGridObjectDelivery); }
        Instantiate(VfxSystem.Instance.vfxDelivered, this.transform.position + Vector3.up * 0.1f, Quaternion.identity);
        PlayerSystem.Instance.gridObjectList.Remove(this);
        Destroy(this.gameObject);
    }
}
