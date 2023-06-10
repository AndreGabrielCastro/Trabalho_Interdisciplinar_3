﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectFacility : GridObject
{
    [Header("Setted during playtime")]
    public UIGridObjectFacility uiGridObjectFacility;
    [SerializeField] private float integrityRegeneration = 0;
    public void StartWork(Worker worker)
    {
        if (currentIntegrityPoints < maximumIntegrityPoints) // If the facility is damaged...
        { integrityRegeneration++; worker.StartRepair(); return; } // Then help repair first
        GetComponent<IFacility>().StartWork();
    }
    public void StartWork()
    {
        if (currentIntegrityPoints < maximumIntegrityPoints) // If the facility is damaged...
        { integrityRegeneration++; return; } // Then help repair first
        GetComponent<IFacility>().StartWork();
    }
    private void StartAllWork()
    {
        foreach (GridTile gridtile in gridTileArray)
        {
            if (gridtile.worker != null)
            {
                gridtile.worker.StopRepair();
                GetComponent<IFacility>().StartWork();
                integrityRegeneration = 0;
            }
        }
    }
    public void StopWork()
    {
        if (currentIntegrityPoints <= maximumIntegrityPoints) // If the facility is damaged...
        { integrityRegeneration--; return; } // Then just leave
        GetComponent<IFacility>().StopWork();
    }
    private void StopAllWork()
    {
        foreach (GridTile gridtile in gridTileArray)
        {
            if (gridtile.worker != null)
            {
                gridtile.worker.StartRepair();
                GetComponent<IFacility>().StopWork();
                integrityRegeneration += 0.5f;
            }
        }
    }
    private void RemoveAllWorkers()
    {
        foreach (GridTile gridtile in gridTileArray)
        {
            if (gridtile.worker != null)
            {
                gridtile.worker.Reset();
                GetComponent<IFacility>().StopWork();
            }
        }
    }
    public override void TakeDamage(int damage)
    {
        if (currentIntegrityPoints == maximumIntegrityPoints) { StopAllWork(); }
        base.TakeDamage(damage);
    }
    public void OnLevelWasLoaded(int level)
    {
        if (level == 0) { uiGridObjectFacility = SpaceShipSystem.Instance.allUiGridObjectFacilityArray[prefabIndex]; }
    }
    public void DeleteGridObjectFacility()
    {
        RemoveAllWorkers();
        foreach (GridTile gridTile in gridTileArray) // Foreach grid tile it occupies...
        { gridTile.SetGridObject(null); } // Set the grid tile to null
        uiGridObjectFacility.AlterateCurrentAmount(+1); // Increases +1 to the UI grid object amount
        Instantiate(Player.Instance.playerFXs.GetVFXGridObjectDeleted(), this.transform.position + Vector3.up * 0.1f, Quaternion.identity);
        Player.Instance.playerAudio.PlaySong(Player.Instance.playerFXs.GetSFXGridObjectDeleted());
        PlayerSystem.Instance.RemoveFromGridObjectList(this);
        Destroy(this.gameObject); // Destroy the object
    }
    public void DestroyGridObjectFacility()
    {
        RemoveAllWorkers();
        foreach (GridTile gridTile in gridTileArray) // Foreach grid tile it occupies...
        { gridTile.SetGridObject(null); } // Set the grid tile to null
        Instantiate(Player.Instance.playerFXs.GetVFXGridObjectDestroyed(), this.transform.position + Vector3.up * 0.1f, Quaternion.identity);
        Player.Instance.playerAudio.PlaySong(Player.Instance.playerFXs.GetSFXGridObjectDestroyed());
        PlayerSystem.Instance.RemoveFromGridObjectList(this);
        Destroy(this.gameObject); // Destroy the object
    }
    private void FixedUpdate()
    {
        if (currentIntegrityPoints == maximumIntegrityPoints) { return; }
        currentIntegrityPoints += (float)integrityRegeneration * Time.fixedDeltaTime;
        UpdateVisual();
        if (currentIntegrityPoints >= maximumIntegrityPoints)
        { 
            currentIntegrityPoints = maximumIntegrityPoints;
            StartAllWork();
        }
    }
}
