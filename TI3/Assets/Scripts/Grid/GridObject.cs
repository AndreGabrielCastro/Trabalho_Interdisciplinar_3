using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public Sprite gridObjectIcon;
    public int gridObjectPrice;
    public int width = 1;
    public int lenght = 1;

    [Header("Must be setted")]
    public int maximumIntegrityPoints;
    public int currentIntegrityPoints;

    [Header("Setted during playtime")]
    public GridTile[] gridTileArray;

    /// <summary>
    /// Sets the grid tiles ocuppied by this grid object
    /// </summary>
    /// <param name="gridTile"></param>
    /// <param name="arrayPosition"></param>
    public void SetGridTile(GridTile gridTile, int arrayPosition) { gridTileArray[arrayPosition] = gridTile; }
    private void Awake() { gridTileArray = new GridTile[width * lenght]; }

    /// <summary>
    /// Causes damage to this Grid Object but doesn't update it's visual. Try the Grid Tile TakeDamage() instead.
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public void TakeDamage(int damage)
    {
        if (currentIntegrityPoints <= 0) { return; }
        currentIntegrityPoints -= damage;
    }
}
