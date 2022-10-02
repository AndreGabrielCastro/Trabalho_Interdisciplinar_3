using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public GameObject gridObject;
    [HideInInspector] public GridVisual gridVisual;
    private void Awake()
    {
        gridVisual = this.GetComponentInChildren<GridVisual>();
    }
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
        GridPosition gridPosition = GridSystem.Instance.GetGridGroundPosition(this.transform.position);
        GridTile gridTile = GridSystem.Instance.TryGetGridTile(gridPosition);

        if (this == gridTile)
        {
            Debug.Log("It's working");
        }
    }

    /// <summary>
    /// Sets the grid object to this grid tile and updates the grid visual color.
    /// </summary>
    public void SetGridObject(GameObject newGridObject)
    {
        this.gridObject = newGridObject;
        if (this.gridObject == null) { gridVisual.SetColorToGreen(); }
        else if (this.gridObject != null) { gridVisual.SetColorToRed(); }
    }
}
