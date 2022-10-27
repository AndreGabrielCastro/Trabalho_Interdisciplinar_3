using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esse script tem a função de executar e realizar todos os cálculos referentes ao grid.
public class GridSystem : MonoBehaviour
{
    public static GridSystem Instance;
    [Tooltip("Must be unpair")] public int width; // Define quantas células em X o grid terá
    [Tooltip("Must be unpair")] public int lenght; // Define quantas células em Z o grid terá
    public float cellsize; // Define o tamanho das células do grid
    public GridTile[,] gridTileArray; // Armazena todos os objetos das células do grid
    [HideInInspector] public int offsetX;
    [HideInInspector] public int offsetZ;
    public LayerMask gridObjectLayerMask = new LayerMask();

    #region CommentedOldCodes
    //private GridEdgeObject[,] gridTopBotEdgeObjectArray; // Armazena todos os objetos dos cantos superiores e inferiroes das células do grid
    //private GridEdgeObject[,] gridLeftRightEdgeObjectArray; // Armazena todos os objetos dos cantos esquerdos e direitos das células do grid
    //[SerializeField] private GameObject gridEdgeObjectVisualPrefab; // Representa o objeto do canto da célula do grid
    //[SerializeField] private GameObject gridTilePrefab; // Célula do grid
    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        offsetX = (int)(width / 2);
        offsetZ = (int)(lenght / 2);
        for (int i = -offsetX; i < offsetX + 2; i++)
        {
            Vector3 vectorFrom = new Vector3(i - 0.5f, 0.01f, -offsetZ - 0.5f);
            Vector3 vectorTo = new Vector3(i - 0.5f, 0.01f, offsetZ + 0.5f);
            Gizmos.DrawLine(vectorFrom, vectorTo);
        }
        for (int j = -offsetZ; j < offsetZ + 2; j++)
        {
            Vector3 vectorFrom = new Vector3(-offsetX - 0.5f, 0.01f, j - 0.5f);
            Vector3 vectorTo = new Vector3(offsetX + 0.5f, 0.01f, j - 0.5f);
            Gizmos.DrawLine(vectorFrom, vectorTo);
        }
    }
    private void Awake()
    {
        #region ErrorTreatment
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Debug.LogError($" ----- There is more than one GridSystem!!! ----- {this.transform.position} ----- {this.gameObject} -----");
            Destroy(this.gameObject);
            return;
        }
        if (width % 2 == 0) { width -= 1; }
        if (lenght % 2 == 0) { lenght -= 1; }
        #endregion

        gridTileArray = new GridTile[width, lenght];
        offsetX = (int)(width / 2);
        offsetZ = (int)(lenght / 2);
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(1) == true) { GetGridObject();}
    }

    /// <summary>
    /// INCOMPLETE Raycasts from camera to mouse position and (try to return) DELETES the grid object.
    /// </summary>
    /// 
    public void GetGridObject()
    {
        GridPosition gridPosition = GetGridGroundPosition(MouseSystem.Instance.GetWorldPosition());
        GridTile gridTile = TryGetGridTile(gridPosition);
        if (gridTile == null) { return; }
        if (gridTile.gridObject == null) { return; }
        if (gridTile.gridObject.TryGetComponent<GridObject>(out GridObject gridObject))
        { DeleteGridObject(gridObject); }
    }

    private void DeleteGridObject(GridObject gridObject)
    {
        foreach(GridTile gridTile in gridObject.gridTileArray)
        { gridTile.SetGridObject(null); }
        gridObject.uiGridObject.UpdateCurrentAmount(+1);
        Destroy(gridObject.gameObject);
    }

    /// <summary>
    /// Returns the world position from the grid position considering offset.
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public Vector3 GetWorldPositionWithOffset(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x + offsetX, 0, gridPosition.z + offsetZ) * cellsize;
    }

    /// <summary>
    /// Returns the world position from the grid position disconsidering offset.
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public Vector3 GetWorldPositionWithoutOffset(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellsize;
    }

    /// <summary>
    /// Returns the world position from the grid position disconsidering offset
    /// </summary>
    /// <param name="gridEdgePosition"></param>
    /// <returns></returns>
    public Vector3 GetWorldPositionWithoutOffset(GridEdgePosition gridEdgePosition)
    {
        return new Vector3(gridEdgePosition.x, 0, gridEdgePosition.z) * cellsize;
    }

    /// <summary>
    /// Returns the grid position from the world position considering grid system position.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public GridPosition GetGridGroundPosition(Vector3 worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt((worldPosition.x - this.transform.position.x) / cellsize), Mathf.RoundToInt((worldPosition.z - this.transform.position.z) / cellsize));
        // OBSERVAÇÃO: (int) nesse caso não daria certo, pois se o valor fosse 0.99, ele seria reduzido a 0. Dessa forma, o valor é arredondado para o inteiro mais próximo
    }

    /// <summary>
    /// Returns the grid float position from the world position considering grid system position
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public GridEdgePosition GetGridGroundFloatPosition(Vector3 worldPosition)
    {
        return new GridEdgePosition(Mathf.RoundToInt((worldPosition.x - this.transform.position.x) / cellsize), Mathf.RoundToInt((worldPosition.z - this.transform.position.z) / cellsize));
    }

    /// <summary>
    /// Returns the vertical grid edge position from the world position disconsidering grid system position and offset
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public GridEdgePosition GetGridEdgeHorizontalPosition(Vector3 worldPosition)
    {
        // Esse código arredonda o X apenas para floats terminados em .5 e o Y para floats de .5 em .5
        float positionX = Mathf.Round(worldPosition.x * 2);
        int positionZ = Mathf.RoundToInt(worldPosition.z);
        float newPositionX = positionX * 0.5f;
        if (positionX % 2 == 0)
        {
            if (worldPosition.x > positionX * 0.5f) { newPositionX += Mathf.Round(0.5f * 2) * 0.5f; }
            else { newPositionX -= Mathf.Round(0.5f * 2) * 0.5f; }
        }
        return new GridEdgePosition(newPositionX, positionZ);
    }

    /// <summary>
    /// Returns the horizontal grid edge position from the world position disconsidering grid system position and offset
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public GridEdgePosition GetGridEdgeVerticalPosition(Vector3 worldPosition)
    {
        // Esse código arredonda o X para floats de .5 em .5 e o Y apenas para floats terminados em .5
        int positionX = Mathf.RoundToInt(worldPosition.x);
        float positionZ = Mathf.Round(worldPosition.z * 2);
        float newPositionZ = positionZ * 0.5f;
        if (positionZ % 2 == 0)
        {
            if (worldPosition.z > positionZ * 0.5f) { newPositionZ += Mathf.Round(0.5f * 2) * 0.5f; }
            else { newPositionZ -= Mathf.Round(0.5f * 2) * 0.5f; }
        }
        return new GridEdgePosition(positionX, newPositionZ);
    }

    /// <summary>
    /// Returns the grid edge corner position from the world position disconsidering grid system position and offset
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public GridEdgePosition GetGridEdgeCornerPosition(Vector3 worldPosition)
    {
        //Esse código arredonda todo e qualquer valor apenas para floats terminados em .5
        float positionX = Mathf.Round(worldPosition.x * 2);
        float positionZ = Mathf.Round(worldPosition.z * 2);
        float newPositionX = positionX * 0.5f;
        float newPositionZ = positionZ * 0.5f;
        if (positionX % 2 == 0)
        {
            if (worldPosition.x > positionX * 0.5f) { newPositionX += Mathf.Round(0.5f * 2) * 0.5f; }
            else { newPositionX -= Mathf.Round(0.5f * 2) * 0.5f; }
        }
        if (positionZ % 2 == 0)
        {
            if (worldPosition.z > positionZ * 0.5f) { newPositionZ += Mathf.Round(0.5f * 2) * 0.5f; }
            else { newPositionZ -= Mathf.Round(0.5f * 2) * 0.5f; }
        }
        return new GridEdgePosition(newPositionX, newPositionZ);
    }

    /// <summary>
    /// Returns the grid tile from the grid position considering offset and grid system position.
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public GridTile TryGetGridTile(GridPosition gridPosition)
    {
        #region ErrorTreatment
        if (gridPosition.x + offsetX < 0 || gridPosition.x + offsetX > width - 1|| // -1 to avoid going out of the bonds of the array
            gridPosition.z + offsetZ < 0 || gridPosition.z + offsetZ > lenght - 1) // -1 to avoid going out of the bonds of the array
        {
            Debug.LogWarning("This grid position is out of the allowed grid range."); return null;
        }
        #endregion

        return gridTileArray[gridPosition.x + offsetX, gridPosition.z + offsetZ];
    }

    /// <summary>
    /// Sets the grid tile in the grid tile array from it's world position considering offset and grid system position.
    /// </summary>
    /// <param name="gridTile"></param>
    public void SetGridTile(GridTile gridTile)
    {
        #region WarningTreatment
        int x = Mathf.RoundToInt(gridTile.transform.position.x - this.transform.position.x) + offsetX;
        int z = Mathf.RoundToInt(gridTile.transform.position.z - this.transform.position.x) + offsetZ;
        if (gridTileArray[x, z] != null)
        {
            Debug.LogWarning("This position already exists in the array. We overwrote it.");
        }
        #endregion

        gridTileArray[x, z] = gridTile;
    }
    
    #region CommentedOldCodes
    //private void GenerateGrid()
    //{
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int z = 0; z < lenght; z++)
    //        {
    //            // Cria os tiles do grid (as "células") e os armazena em seu respectivo array
    //            GridPosition gridPosition = new GridPosition(x, z);
    //            gridTileArray[x, z] = Instantiate(gridTilePrefab, GetWorldPosition(gridPosition), Quaternion.identity);
    //        }
    //    }

    //    gridLeftRightEdgeObjectArray = new GridEdgeObject[width + 1, lenght];
    //    gridTopBotEdgeObjectArray = new GridEdgeObject[width, lenght + 1];
    //    for (int x = 0; x < width + 1; x++)
    //    {
    //        for (int z = 0; z < lenght; z++)
    //        {
    //            // Cria os visuais dos cantos do grid (os cantos das "células")
    //            GridEdgePosition gridLeftRightEdgePosition = new GridEdgePosition(Mathf.Round((x - (cellsize * 0.5f)) * 10f) * 0.1f, Mathf.Round(z)); // Arredonda para a integral mais próxima e mantém 1 decimal
    //            gridLeftRightEdgeObjectArray[x, z] = new GridEdgeObject(gridLeftRightEdgePosition);
    //            Instantiate(gridEdgeObjectVisualPrefab, GetWorldPosition(gridLeftRightEdgePosition), Quaternion.identity);
    //        }
    //    }

    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int z = 0; z < lenght + 1; z++)
    //        {
    //            // Cria os visuais dos cantos do grid (os cantos das "células")
    //            GridEdgePosition gridTopBotPosition = new GridEdgePosition(Mathf.Round(x), Mathf.Round((z - (cellsize * 0.5f)) * 10f) * 0.1f); // Arredonda para a integral mais próxima e mantém 1 decimal
    //            gridTopBotEdgeObjectArray[x, z] = new GridEdgeObject(gridTopBotPosition);
    //            Instantiate(gridEdgeObjectVisualPrefab, GetWorldPosition(gridTopBotPosition), Quaternion.identity);
    //        }
    //    }
    //}
    //public Vector3 GetWorldPosition(GridEdgePosition gridEdgePosition) // Retorna a posição no mundo a partir da posição no grid
    //{
    //    return new Vector3(gridEdgePosition.x, 0, gridEdgePosition.z) * cellsize; // Retorna a posição no mundo da célula
    //}
    //public GridEdgePosition GetGridEdgePosition(Vector3 worldPosition) // Retorna a posição no canto do grid a partir da posição no mundo
    //{
    //    Esse código arredonda todo e qualquer valor apenas para floats terminados em .5
    //    Debug.Log($"WorldPosition: {worldPosition.x} {worldPosition.z}");
    //    float positionX = Mathf.Round(worldPosition.x * 2);
    //    float positionZ = Mathf.Round(worldPosition.z * 2);
    //    float newPositionX = positionX * 0.5f;
    //    float newPositionZ = positionZ * 0.5f;
    //    if (positionX % 2 == 0)
    //    {
    //        if (worldPosition.x > positionX * 0.5f) { newPositionX += Mathf.Round(0.5f * 2) * 0.5f; }
    //        else { newPositionX -= Mathf.Round(0.5f * 2) * 0.5f; }
    //    }
    //    if (positionZ % 2 == 0)
    //    {
    //        if (worldPosition.z > positionZ * 0.5f) { newPositionZ += Mathf.Round(0.5f * 2) * 0.5f; }
    //        else { newPositionZ -= Mathf.Round(0.5f * 2) * 0.5f; }
    //    }
    //    Debug.Log($"gridPosition: {newPositionX} {newPositionZ}");
    //    return new GridEdgePosition(newPositionX, newPositionZ);
    //}
    //public GridEdgeObject GetGridEdgeObject(GridEdgePosition gridEdgePosition) // Retorna o objeto do canto do grid a partir da posição no grid
    //{
    //    float positionX = Mathf.Round(gridEdgePosition.x * 2);
    //    float positionZ = Mathf.Round(gridEdgePosition.z * 2);
    //    if (positionX % 2 == 0)
    //    {
    //        return gridTopBotEdgeObjectArray[Mathf.RoundToInt(gridEdgePosition.x), Mathf.RoundToInt(gridEdgePosition.z + 0.5f)];
    //    }
    //    else if (positionZ % 2 == 0)
    //    {
    //        return gridLeftRightEdgeObjectArray[Mathf.RoundToInt(gridEdgePosition.x + 0.5f), Mathf.RoundToInt(gridEdgePosition.z)];
    //    }
    //    else
    //    {
    //        Debug.LogError($" ----- This condition shouldn't be reached by any means ----- {this.transform} ----- {this.gameObject} -----");
    //        return null;
    //    }
    //}
    #endregion
}

/// <summary>
/// Representation of the positions in the grid system previously created.
/// </summary>
public struct GridPosition : System.IEquatable<GridPosition>
{
    public int x;
    public int z;
    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
    public GridPosition(GridEdgePosition gridEdgePosition)
    {
        this.x = (int)gridEdgePosition.x;
        this.z = (int)gridEdgePosition.z;
    }
    public override string ToString()
    {
        return ($"X: {x} / Z: {z}");
    }
    #region operadores
    public static bool operator ==(GridPosition a, GridPosition b)
    {

        return a.x == b.x && a.z == b.z; // Essa linha equivale ao código abaixo
        /* 
        if (a.x == b.x & a.z == b.z)
        {
            return true;
        }
        else
        {
            return false;
        }
        */
    }
    public static bool operator !=(GridPosition a, GridPosition b)
    {
        return !(a == b); // Essa linha equivale ao código abaixo
        /*
        if (a != b)
        {
            return true;
        }
        else
        {
            return false;
        }
        */
    }
    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x + b.x, a.z + b.z);
    }
    public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x - b.x, a.z - b.z);
    }
    #endregion
    #region outros
    // Ações rápidas e metaforações, gerar Equals e GetHashCode. Não faço ideia do q q isso faz
    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }
    public override int GetHashCode()
    {
        int hashCode = 1553271884;
        hashCode = hashCode * -1521134295 + x.GetHashCode();
        hashCode = hashCode * -1521134295 + z.GetHashCode();
        return hashCode;
    }
    // Ações rápidas e metaforações, implementar interface System.IEquatable. Não faço ideia do q q isso faz
    public bool Equals(GridPosition other)
    {
        return this == other; // Essa linha equivale ao código abaixo
        /*
        if (this == other)
        {
            return true;
        }
        else
        {
            return false;
        }
        */
    }
    #endregion
}

/// <summary>
/// Representation of the grid edge positions in the grid system previously created
/// </summary>
public struct GridEdgePosition : System.IEquatable<GridEdgePosition>
{
    public float x;
    public float z;
    public GridEdgePosition(float x, float z)
    {
        this.x = x;
        this.z = z;
    }
    public GridEdgePosition(GridPosition gridPosition)
    {
        this.x = gridPosition.x;
        this.z = gridPosition.z;
    }
    public override string ToString()
    {
        return ($"X: {x} / Z: {z}");
    }
    #region operadores
    public static bool operator ==(GridEdgePosition a, GridEdgePosition b)
    {

        return a.x == b.x && a.z == b.z; // Essa linha equivale ao código abaixo
        /* 
        if (a.x == b.x & a.z == b.z)
        {
            return true;
        }
        else
        {
            return false;
        }
        */
    }
    public static bool operator !=(GridEdgePosition a, GridEdgePosition b)
    {
        return !(a == b); // Essa linha equivale ao código abaixo
        /*
        if (a != b)
        {
            return true;
        }
        else
        {
            return false;
        }
        */
    }
    public static GridEdgePosition operator +(GridEdgePosition a, GridEdgePosition b)
    {
        return new GridEdgePosition(a.x + b.x, a.z + b.z);
    }
    public static GridEdgePosition operator -(GridEdgePosition a, GridEdgePosition b)
    {
        return new GridEdgePosition(a.x - b.x, a.z - b.z);
    }
    #endregion
    #region outros
    // Ações rápidas e metaforações, gerar Equals e GetHashCode. Não faço ideia do q q isso faz
    public override bool Equals(object obj)
    {
        return obj is GridEdgePosition position &&
               x == position.x &&
               z == position.z;
    }
    public override int GetHashCode()
    {
        int hashCode = 1553271884;
        hashCode = hashCode * -1521134295 + x.GetHashCode();
        hashCode = hashCode * -1521134295 + z.GetHashCode();
        return hashCode;
    }
    // Ações rápidas e metaforações, implementar interface System.IEquatable. Não faço ideia do q q isso faz
    public bool Equals(GridEdgePosition other)
    {
        return this == other; // Essa linha equivale ao código abaixo
        /*
        if (this == other)
        {
            return true;
        }
        else
        {
            return false;
        }
        */
    }
    #endregion
}

#region CommentedOldCodes
//public class GridEdgeObject
//{
//    private GridEdgePosition gridEdgePosition;
//    private GameObject gridEdgeObject;
//    public GridEdgeObject(GridEdgePosition gridEdgePosition)
//    {
//        this.gridEdgePosition = gridEdgePosition;
//    }
//}
#endregion