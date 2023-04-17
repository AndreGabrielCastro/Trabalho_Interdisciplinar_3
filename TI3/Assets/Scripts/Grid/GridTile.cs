using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public Worker worker;
    [HideInInspector] public GridObject gridObject;
    [HideInInspector] public GridVisual gridVisual;
    private void Awake() { gridVisual = this.GetComponentInChildren<GridVisual>(); }
    void Start()
    {
        #region ErrorTreatment
        if (this.transform.localPosition.x < -GridSystem.Instance.offsetX || this.transform.localPosition.x > GridSystem.Instance.offsetX ||
            this.transform.localPosition.z < -GridSystem.Instance.offsetZ || this.transform.localPosition.z > GridSystem.Instance.offsetZ)
        {
            Debug.LogWarning($"This grid tile is out of the allowed grid range. Position: {this.transform.localPosition}"); return;
        }
        #endregion

        GridSystem.Instance.SetGridTile(this);

        #region Log
        //GridPosition gridPosition = GridSystem.Instance.GetGridGroundPosition(this.transform.position);
        //GridTile gridTile = GridSystem.Instance.TryGetGridTile(gridPosition);
        //if (this == gridTile) { Debug.Log("It's working"); }
        #endregion
    }
    public void SetWorker(Worker worker)
    {
        this.worker = worker;
    }

    /// <summary>
    /// Sets the grid object of this grid tile and updates the grid visual color.
    /// </summary>
    public void SetGridObject(GridObject newGridObject)
    {
        this.gridObject = newGridObject;
        if (this.gridObject == null) { gridVisual.SetColorToWhite(); }
        else if (this.gridObject != null) { gridVisual.SetColorToGreen(); }
    }

    /// <summary>
    /// Cause damage to the Grid Object of this Grid Tile and updates the Grid Visual
    /// </summary>
    public void TakeDamage(int damage)
    {
        if (gridObject == null) { return; }
        gridObject.TakeDamage(damage);
    }
    //Color(1f, 0.5f, 0.5f) red
    //Color(0.5f, 1f, 0.5f) green
}
