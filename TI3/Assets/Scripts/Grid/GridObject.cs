using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public Sprite gridObjectIcon;
    public int gridObjectPrice;
    public int width = 1;
    public int lenght = 1;
    public GridTile[] gridTileArray;
    public UIGridObject uiGridObject;

    private void Awake()
    {
        gridTileArray = new GridTile[width * lenght];
    }

    public void SetGridTile(GridTile gridTile, int arrayPosition) { gridTileArray[arrayPosition] = gridTile; }
}
