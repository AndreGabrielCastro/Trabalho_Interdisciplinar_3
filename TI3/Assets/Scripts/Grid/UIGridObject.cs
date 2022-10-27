using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// Esse script tem a função de armazenar as informações da instalação que será criada pelo jogador. Tudo isso é feito automaticamente no Awake().
// A maioria dos códigos nesse script são para fins de Game Juice. Suavizar transição e evitar Z fight, por exemplo.
// Então, para evitar perda de performance, maioria desses ajustes são feitos no Awake() para já deixar os componentes necessários referenciados.
// O PreviewInstance precisa ser referenciado na hora para evitar futuras complicações. Se eu receber pelo Instantiate() o objeto será criado e armazenado.
// Para fins de performance, esse método pode não ser o ideal se tivermos 20 instalações para serem criadas, pois todas elas "existiriam", de certa forma.
public class UIGridObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Tooltip("Must be a GridObject")]
    public GameObject gridObjectPrefab; // This thing MUST be GameObject to work with Awake(), otherwise it will log error on the first line of Awake();
    public int maxAmount = 1;
    public int curAmount = 1;
    public TMP_Text curAmountText;


    // References to be setted on Awake() down below
    private GameObject gameObjectPreview;
    private GridObject gridObjectPrefabScript;
    private int gridObjectPrefabScriptWidth;
    private int gridObjectPrefabScriptLength;
    private Snap snap;

    private GameObject gridObjectPrefabPreviewInstance;
    private MeshRenderer gridObjectPrefabPreviewInstanceRenderer;

    private Vector3 desiredWorldPosition;
    private Quaternion desiredWorldRotation;
    private int desiredWidth;
    private int desiredLength;
    private Snap desiredSnap;

    private bool isDragging;

    public enum Snap
    {
        ToGround,
        ToEdgeHorizontal,
        ToEdgeVertical,
        ToEdgeCorner,
    }

    public void UpdateCurrentAmount(int valueToIncreaseOrDecrease)
    {
        curAmount += valueToIncreaseOrDecrease;
        curAmountText.text = curAmount.ToString();
    }

    /// <summary>
    /// Switches the desired width value with the desired length value.
    /// </summary>
    private void SwitchDesiredWidthAndLength() { int temp = desiredWidth; desiredWidth = desiredLength; desiredLength = temp; }
    private void Awake()
    {
        this.gridObjectPrefabScript = gridObjectPrefab.GetComponent<GridObject>(); // Gets the grid object script and stores it
        this.gridObjectPrefabScriptWidth = gridObjectPrefabScript.width; // Gets the grid object width and stores it
        this.gridObjectPrefabScriptLength = gridObjectPrefabScript.lenght; // Gets the grid object lenght and stores it
        this.gameObject.GetComponent<Image>().sprite = gridObjectPrefabScript.gridObjectIcon; // Gets the grid object icon and applies it
        this.gameObjectPreview = gridObjectPrefab.transform.Find("Mesh").gameObject; // Gets the grid object mesh game object and stores it

        this.gridObjectPrefabScript.uiGridObject = this; // Sets the UIGridObject of the gridObject. This code is to make easier to recover

        if (gridObjectPrefabScriptWidth % 2 == 1 && gridObjectPrefabScriptLength % 2 == 1) { snap = Snap.ToGround; } // If width and length are both unpair, snap to ground
        else if (gridObjectPrefabScriptWidth % 2 == 0 && gridObjectPrefabScriptLength % 2 != 0) { snap = Snap.ToEdgeHorizontal; } // If width is pair and length unpair, snap to left or right edge
        else if (gridObjectPrefabScriptWidth % 2 != 0 && gridObjectPrefabScriptLength % 2 == 0) { snap = Snap.ToEdgeVertical; } // if width is unpair and length pair, snap to top or bot edge
        else if (gridObjectPrefabScriptWidth % 2 == 0 && gridObjectPrefabScriptLength % 2 == 0) { snap = Snap.ToEdgeCorner; } // If width and length are both pair, snap to corner
        else { Debug.LogError($"Please, check UIGridObject cause you've just done something IMPOSSIBLE. ---{this.transform.position}---"); }
    }
    private void Update()
    {
        if (isDragging == false) { return; } // Avoid the code from running when not dragging any object
        if (Input.mouseScrollDelta.y > 0) // If the mouse wheel scrolls forward...
        {
            desiredWorldRotation.eulerAngles += new Vector3(0, 90, 0); // Sets desired rotation to +90 degrees
            if (desiredSnap == Snap.ToEdgeHorizontal) // If the snap is horizontal...
            { 
                desiredSnap = Snap.ToEdgeVertical; // Changes it to vertical
                SwitchDesiredWidthAndLength(); // Switches desired width and length values
            }
            else if (desiredSnap == Snap.ToEdgeVertical)
            {
                desiredSnap = Snap.ToEdgeHorizontal; // Changes it to horizontal
                SwitchDesiredWidthAndLength(); // Switches desired width and length values
            }
        } // Set desired rotation to +90 degrees, switches the snap and switch desired width and desired length values
        else if (Input.mouseScrollDelta.y < 0) // If the mouse wheel scrolls backward...
        {
            desiredWorldRotation.eulerAngles -= new Vector3(0, 90, 0);
            if (desiredSnap == Snap.ToEdgeHorizontal)
            {
                desiredSnap = Snap.ToEdgeVertical; // Changes it to vertical
                SwitchDesiredWidthAndLength(); // Switches desired width and length values
            }
            else if (desiredSnap == Snap.ToEdgeVertical)
            {
                desiredSnap = Snap.ToEdgeHorizontal; // Changes it to horizontal
                SwitchDesiredWidthAndLength(); // Switches desired width and length values
            }
        } // Set desired rotation to -90 degrees and switches the snap
    }
    private void FixedUpdate()
    {
        if (isDragging == false) { return; } // Avoid the code from running when not dragging any object
        gridObjectPrefabPreviewInstance.transform.position = Vector3.Lerp(gridObjectPrefabPreviewInstance.transform.position, desiredWorldPosition, Time.deltaTime * 10); // Moves object preview to the desired position smoothly

        if (gridObjectPrefabPreviewInstance.transform.rotation == desiredWorldRotation) { return; } // Avoid the code from running when not desired
        gridObjectPrefabPreviewInstance.transform.rotation = Quaternion.Lerp(gridObjectPrefabPreviewInstance.transform.rotation, desiredWorldRotation, Time.fixedDeltaTime * 10); // Rotates the object preview to the desired rotation smoothly
    }

    /// <summary>
    /// Executes when the mouse is clicked over this selectable object.
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (pointerEventData.button != PointerEventData.InputButton.Left) { return; } // If the click isn't from left mouse button, returns

        if (curAmount == 0) { return; }
        else if (curAmount < 0) { Debug.LogWarning("Current amount below zero, something is wrong!"); }

        // VERIFICAR A DISPONIBILIDADE DE RECURSO AQUI

        // This body of code avoids the object from being created in the sky.
        Vector3 worldPosition = MouseSystem.Instance.GetWorldPosition(); // Gets the world position of the mouse
        GridPosition gridPosition = GridSystem.Instance.GetGridGroundPosition(worldPosition); // Gets the grid position from the world position
        worldPosition = GridSystem.Instance.GetWorldPositionWithoutOffset(gridPosition); // Rounds the world position to grid position

        // This body of code sets important variables
        desiredWorldPosition = worldPosition; // Without this there will be a "glitch" every click. The object will "flick" it's position from nowhere.
        desiredWorldRotation = Quaternion.identity; // Resets the desired rotation
        this.desiredWidth = this.gridObjectPrefabScriptWidth; // Sets the desired width to use instead of the original width so every object starts the same way
        this.desiredLength = this.gridObjectPrefabScriptLength; // Sets the desired length to use instead of the original length so every object starts the same way
        desiredSnap = snap; // Sets the desired snap to use instead of the original snap so every object starts the same way
        isDragging = true; // Activates the smooth in FixedUpdate() and enables rotations in the Update();

        // This body of code creates and instantiates the preview and scales it a little bigger to avoid Z Fight
        gridObjectPrefabPreviewInstance = Instantiate(gameObjectPreview, worldPosition, Quaternion.identity); // Creates the object at the mouse position and stores it
        gridObjectPrefabPreviewInstance.transform.localScale = gridObjectPrefabPreviewInstance.transform.localScale * 1.01f; // Scales it a little bigger to avoid Z fight
        gridObjectPrefabPreviewInstanceRenderer = gridObjectPrefabPreviewInstance.GetComponentInChildren<MeshRenderer>(); // REMOVER INCHILDREN FUTURAMENTE
        Debug.Log("Sometimes the object may be fully scaled to 1. if this happens, change and change back the scale of the object in the prefab");
    }

    /// <summary>
    /// Executes when the mouse click on this selectable object is dragged.
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnDrag(PointerEventData pointerEventData)
    {
        if (pointerEventData.button != PointerEventData.InputButton.Left) { return; }

        if (curAmount == 0) { return; }
        else if (curAmount < 0) { Debug.LogWarning("Current amount below zero, something is wrong!"); }

        // This body of code returns the variables needed to the verification right below. It's maximum optimized, don't worry.
        Vector3 worldPosition = MouseSystem.Instance.GetWorldPosition(); // Gets the world position of the mouse
        GridEdgePosition gridFloatPosition = new GridEdgePosition(0,0); // Initializes gridEdgePosition

        if (desiredSnap == Snap.ToGround)
        { gridFloatPosition = GridSystem.Instance.GetGridGroundFloatPosition(worldPosition); } // Gets the ground grid position from the world position
        else if (desiredSnap == Snap.ToEdgeHorizontal)
        { gridFloatPosition = GridSystem.Instance.GetGridEdgeHorizontalPosition(worldPosition); } // Gets the horizontal grid edge position from the world position
        else if (desiredSnap == Snap.ToEdgeVertical)
        { gridFloatPosition = GridSystem.Instance.GetGridEdgeVerticalPosition(worldPosition); } // Gets the vertical grid edge position from the world position
        else if (desiredSnap == Snap.ToEdgeCorner)
        { gridFloatPosition = GridSystem.Instance.GetGridEdgeCornerPosition(worldPosition); } // Gets the corner grid edge position from the world position

        worldPosition = GridSystem.Instance.GetWorldPositionWithoutOffset(gridFloatPosition); // Gets the world position from the grid edge position
        desiredWorldPosition = worldPosition; // Sets the desired world position. This is necessary to smoothly change the position in FixedUpdate()

        gridFloatPosition -= new GridEdgePosition(((float)desiredWidth - 1) / 2, ((float)desiredLength - 1) / 2); // Modifies the grid float position considering the width and length
        GridPosition gridPosition = new GridPosition(gridFloatPosition); // Gets the grid object from the grid float position

        // This "for" goes through all tiles it occupies and verifies if they exists or if they're available
        for (int x = 0; x < desiredWidth; x++) // Goes through each line
        {
            for (int z = 0; z < desiredLength; z++) // Goes through each column
            {
                GridTile gridTile = GridSystem.Instance.TryGetGridTile(gridPosition + new GridPosition(x, z)); // Try to get the grid tile from the grid position considering line and column

                if (gridTile == null) { gridObjectPrefabPreviewInstanceRenderer.material.SetColor("_BaseColor", Color.black); return; } // Turns the renderer to black and return;

                if (gridTile.gridObject == null) // if the grid tile is available...
                {
                    Color whiteColor = new Color(0.7f, 0.7f, 0.7f, 1);
                    gridObjectPrefabPreviewInstanceRenderer.material.SetColor("_BaseColor", whiteColor); // Turns the renderer color to white
                }
                else if (gridTile.gridObject != null) // if the grid tile is occupied...
                {
                    Color blackColor = new Color(0.3f, 0.3f, 0.3f, 1);
                    gridObjectPrefabPreviewInstanceRenderer.material.SetColor("_BaseColor", blackColor); // Turns the renderer color to black
                    return;
                }
                // AQUI HÁ UM PEQUENO GLITCH. SE O IF ENCONTRAR A COR CINZA PRIMEIRO ELE NÃO VAI TORNÁ-LA PRETA MESMO SE ESTIVER FORA DO GRID
            }
        }

        #region CommentedOldCodes
        //Vector3 worldPosition = MouseSystem.Instance.GetWorldPosition(); // Gets the world position of the mouse
        //GridPosition gridPosition = GridSystem.Instance.GetGridGroundPosition(worldPosition); // Gets the grid position from the world position
        //worldPosition = GridSystem.Instance.GetWorldPositionWithoutOffset(gridPosition); // Rounds the world position to grid position

        //GridTile gridTile = GridSystem.Instance.TryGetGridTile(gridPosition); // Gets the grid object from the grid tile of the grid position
        //desiredWorldPosition = worldPosition; // Sets the desired world position. This is necessary to smoothly change the position in FixedUpdate()

        //desiredWorldPosition += new Vector3((gridObjectPrefabScriptWidth - 1) * 0.5f, 0, (gridObjectPrefabScriptLenght - 1) * 0.5f);

        //gridObjectPrefabPreviewInstance.transform.position = worldPosition; // This will instantly change the position

        //if (gridTile == null) { gridObjectPrefabPreviewInstanceRenderer.material.SetColor("_BaseColor", Color.black); return; } // Turns the renderer to black and return;

        //if (gridTile.gridObject == null) // if the grid tile is available...
        //{
        //    Color whiteColor = new Color(0.7f, 0.7f, 0.7f, 1);
        //    gridObjectPrefabPreviewInstanceRenderer.material.SetColor("_BaseColor", whiteColor); // Turns the renderer color to white
        //}
        //else if (gridTile.gridObject != null) // if the grid tile is occupied...
        //{
        //    Color blackColor = new Color(0.3f, 0.3f, 0.3f, 1);
        //    gridObjectPrefabPreviewInstanceRenderer.material.SetColor("_BaseColor", blackColor); // Turns the renderer color to black
        //}
        #endregion
    }

    /// <summary>
    /// Executes when the mouse click on this selectable object is released.
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (pointerEventData.button != PointerEventData.InputButton.Left) { return; }

        if (curAmount == 0) { return; }
        else if (curAmount < 0) { Debug.LogWarning("Current amount below zero, something is wrong!"); }

        // This body of code deactivate some functions
        isDragging = false; // Deactivates the smooth in the FixedUpdate() and disables rotations in Update();
        Destroy(gridObjectPrefabPreviewInstance); // Get rid of the preview
        Quaternion worldRotation = desiredWorldRotation; // Gets the world rotation before reset

        // This body of code returns the variables needed to the verification right below. It's maximum optimized, don't worry.
        Vector3 worldPosition = MouseSystem.Instance.GetWorldPosition(); // Gets the world position of the mouse
        GridEdgePosition gridFloatPosition = new GridEdgePosition(0, 0); // Initializes gridEdgePosition

        if (desiredSnap == Snap.ToGround)
        { gridFloatPosition = GridSystem.Instance.GetGridGroundFloatPosition(worldPosition); } // Gets the ground grid position from the world position
        else if (desiredSnap == Snap.ToEdgeHorizontal)
        { gridFloatPosition = GridSystem.Instance.GetGridEdgeHorizontalPosition(worldPosition); } // Gets the horizontal grid edge position from the world position
        else if (desiredSnap == Snap.ToEdgeVertical)
        { gridFloatPosition = GridSystem.Instance.GetGridEdgeVerticalPosition(worldPosition); } // Gets the vertical grid edge position from the world position
        else if (desiredSnap == Snap.ToEdgeCorner)
        { gridFloatPosition = GridSystem.Instance.GetGridEdgeCornerPosition(worldPosition); } // Gets the corner grid edge position from the world position

        worldPosition = GridSystem.Instance.GetWorldPositionWithoutOffset(gridFloatPosition); // Gets the world position from the grid edge position
        desiredWorldPosition = worldPosition; // Sets the desired world position. This is necessary to smoothly change the position in FixedUpdate()

        gridFloatPosition -= new GridEdgePosition(((float)desiredWidth - 1) / 2, ((float)desiredLength - 1) / 2); // Modifies the grid float position considering the width and length
        GridPosition gridPosition = new GridPosition(gridFloatPosition); // Gets the grid object from the grid float position

        // This "for" goes through all the tiles it occupies and verifies if they exists and if they're all available
        for (int x = 0; x < desiredWidth; x++) // Goes through each line
        {
            for (int z = 0; z < desiredLength; z++) // Goes through each column
            {
                GridTile gridTile = GridSystem.Instance.TryGetGridTile(gridPosition + new GridPosition(x, z)); // Try to get the grid tile from the grid position considering line and column
                if (gridTile == null) { return; } // If the grid tile does not exist, return;
                if (gridTile.gridObject != null) { Debug.Log("Can't place over another object!"); return; } // If the grid tile is occupied, debug and return
            }
        }

        GameObject newGameObject = Instantiate(gridObjectPrefab, worldPosition, worldRotation); // Creates and instance of the grid object
        GridObject newGridObject = newGameObject.GetComponent<GridObject>(); // Gets the reference of the GridObject script of the grid object

        // CONSUMIR OS RECURSOS AQUI

        // This "for" goes through all the tiles it occupies and sets their grid object to the previously instantiated grid object
        int arrayPosition = 0;
        for (int x = 0; x < desiredWidth; x++) // Goes through each line
        {
            for (int z = 0; z < desiredLength; z++) // Goes through each column
            {
                GridTile gridTile = GridSystem.Instance.TryGetGridTile(gridPosition + new GridPosition(x, z)); // Try to get the grid tile from the grid position considering line and column
                gridTile.SetGridObject(newGridObject); // Sets the grid object to the grid tile and updates the grid visual
                newGridObject.SetGridTile(gridTile, arrayPosition); // Sets the grid tiles the grid object occupies (Mainly used to easily delete the grid object later)
                arrayPosition += 1; // Increases the array position
            }
        }

        UpdateCurrentAmount(-1);

        #region CommentedOldCodes
        // This body of code deactivate some functions
        //isDragging = false; // Deactivates the smooth in the FixedUpdate() and disables rotations in Update();
        //Destroy(gridObjectPrefabPreviewInstance); // Get rid of the preview
        //Quaternion worldRotation = desiredWorldRotation; // Gets the world rotation before reset
        //desiredWorldRotation = Quaternion.identity; // Resets the desired world rotation

        //// This body of code returns the variables needed to the verification right below. It's maximum optimized, don't worry.
        //Vector3 worldPosition = MouseSystem.Instance.GetWorldPosition(); // Gets the world position of the mouse
        //GridPosition gridPosition = GridSystem.Instance.GetGridGroundPosition(worldPosition); // Gets the grid position from the world position
        //worldPosition = GridSystem.Instance.GetWorldPositionWithoutOffset(gridPosition); // Rounds the world position to grid position

        //GridTile gridTile = GridSystem.Instance.TryGetGridTile(gridPosition); // Gets the grid object from the grid tile of the grid position

        //if (gridTile == null) { return; } // If the grid tile doesn't exist, returns

        //if (gridTile.gridObject == null) // If the grid tile is available...
        //{
        //    GameObject newGridObject = Instantiate(gridObjectPrefab, worldPosition, worldRotation); // Creates and instance of the grid object
        //    gridTile.SetGridObject(newGridObject); // Sets the grid object to the grid tile and updates the grid visual

        //    // CONSUMIR OS RECURSOS AQUI
        //}
        //else if (gridTile.gridObject != null) // If the grid tile is occupied...
        //{
        //    Debug.Log("Can't place over another object!");
        //}
        #endregion
    }
}