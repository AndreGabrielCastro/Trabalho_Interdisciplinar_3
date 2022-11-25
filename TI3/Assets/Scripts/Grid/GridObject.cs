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
        if (currentIntegrityPoints <= 0)
        { 
            if (this.gameObject.TryGetComponent<GridObjectDelivery>(out GridObjectDelivery gridObjectDelivery) == true)
            { gridObjectDelivery.DestroyGridObjectDelivery(); }
            else if (this.gameObject.TryGetComponent<GridObjectFacility>(out GridObjectFacility gridObjectFacility) == true)
            { gridObjectFacility.DestroyGridObjectFacility(); }
            return;
        }
        currentIntegrityPoints -= damage;

        float value = 0.5f * ((float)currentIntegrityPoints / (float)maximumIntegrityPoints);
        Color currentIntegrityColor = new Color(1f - value, 0.5f + value, 0.5f);

        for (int i = 0; i < gridTileArray.Length; i++)
        { gridTileArray[i].gridVisual.SetColorTo(currentIntegrityColor); }
    }

    public void HealDamage(int heal)
    {
        if (currentIntegrityPoints >= maximumIntegrityPoints) { return; }
        currentIntegrityPoints += heal;

        float value = 0.5f * ((float)currentIntegrityPoints / (float)maximumIntegrityPoints);
        Color currentIntegrityColor = new Color(1f - value, 0.5f + value, 0.5f);

        for (int i = 0; i < gridTileArray.Length; i++)
        { gridTileArray[i].gridVisual.SetColorTo(currentIntegrityColor); }
    }
}
