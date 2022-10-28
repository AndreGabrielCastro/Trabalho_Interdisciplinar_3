using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIRouteSystem : MonoBehaviour
{
    public static UIRouteSystem Instance;

    public Transform uiUserInterface;
    public UIUserInterface uiUserInterfaceMain;
    public UIFloatingText uiFloatingTextPrefab;
    public UIFader uiFader;

    public UIRoute activeRoute;

    private int currentColonyIndex;
    private int destinationColonyIndex;

    private bool isLoading = false;
    private float timer = 1;
    private void Awake()
    {
        #region ErrorTreatment
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Debug.LogError($" ----- There is more than one UIRouteSystem!!! ----- {this.transform.position} ----- {this.gameObject} -----");
            Destroy(this.gameObject);
            return;
        }
        #endregion
    }
    private void Start() { currentColonyIndex = ColonySystem.Instance.currentColonyIndex; }
    private void FixedUpdate()
    {
        if (isLoading == false) { return; }
        timer -= Time.fixedDeltaTime;
        if (timer <= 0) { UnityEngine.SceneManagement.SceneManager.LoadScene("ColonyScene"); }
    }
    public void UpdateSelectedRoute(UIRoute uiRoute)
    {
        if (currentColonyIndex != uiRoute.firstColonyIndex && currentColonyIndex != uiRoute.secondColonyIndex) // If route is out of range...
        {
            uiRoute.image.color = new Color(1f, 0.4f, 0.5f, 0.7f); // Sets the color of the route to red
        }
        else if (currentColonyIndex != uiRoute.firstColonyIndex && currentColonyIndex == uiRoute.secondColonyIndex)
        {
            if (activeRoute != null) { activeRoute.image.color = new Color(0.3f, 1f, 0.6f, 0.7f); } // Sets the color of the previous active route to green
            uiRoute.image.color = new Color(0.3f, 1f, 1f, 0.7f); // Sets the color of the route to cyan
            activeRoute = uiRoute; // Sets the route as the new active route
            destinationColonyIndex = uiRoute.firstColonyIndex; // Stores the route's destination index
        }
        else if (currentColonyIndex == uiRoute.firstColonyIndex && currentColonyIndex != uiRoute.secondColonyIndex)
        {
            if (activeRoute != null) { activeRoute.image.color = new Color(0.3f, 1f, 0.6f, 0.7f); } // Sets the color of the previous active route to green
            uiRoute.image.color = new Color(0.3f, 1f, 1f, 0.7f); // Sets the color of the route to cyan
            activeRoute = uiRoute; // Sets the route as the new active route
            destinationColonyIndex = uiRoute.secondColonyIndex; // Stores the route's destination index
        }
        else if (currentColonyIndex == uiRoute.firstColonyIndex && currentColonyIndex == uiRoute.secondColonyIndex) // If route has same indexes...
        {
            Debug.LogError($"One UIRoute got 2 equal colony index!!!!! ----- {uiRoute.transform.position} -----"); return;
        }
    }
    public void OnButtonTravel()
    {
        if (activeRoute == null)
        {
            UIFloatingText uiFloatingText = Instantiate(uiFloatingTextPrefab, Input.mousePosition, Quaternion.identity);
            uiFloatingText.transform.SetParent(uiUserInterface);
            uiFloatingText.transform.localScale = Vector3.one;
            TMP_Text text = uiFloatingText.GetComponent<TMP_Text>();
            text.color = Color.red;
            text.text = "Select a valid route first!";
        }
        else if (activeRoute != null)
        {
            uiUserInterfaceMain.OnButtonLerpToDown(); // Lerps the screen back to the SpaceShip
            uiFader.FadeIn(); // Activates the fade in
            isLoading = true; // Activates the timer
            PlayerData.Instance.SetCurrentColony(destinationColonyIndex); // Sets the current colony of the player
        }
    }
}