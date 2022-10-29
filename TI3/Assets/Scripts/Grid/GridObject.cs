using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public Sprite gridObjectIcon;
    public int gridObjectPrice;
    public int width = 1;
    public int lenght = 1;

    [Header("Setted during playtime")]
    public GridTile[] gridTileArray;

    /// <summary>
    /// Sets the grid tiles ocuppied by this grid object
    /// </summary>
    /// <param name="gridTile"></param>
    /// <param name="arrayPosition"></param>
    public void SetGridTile(GridTile gridTile, int arrayPosition) { gridTileArray[arrayPosition] = gridTile; }
    private void Awake() { gridTileArray = new GridTile[width * lenght]; }
}
