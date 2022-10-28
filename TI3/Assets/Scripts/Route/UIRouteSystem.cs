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
    public UIRoute selectedRoute;

    private int currentColonyIndexPosition;
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
    private void Start() { currentColonyIndexPosition = ColonySystem.Instance.currentColonyIndexPosition; }
    public void UpdateSelectedRoute(UIRoute uiRoute)
    {
        this.selectedRoute = uiRoute;
        if (currentColonyIndexPosition != uiRoute.firstColonyIndex && currentColonyIndexPosition != uiRoute.secondColonyIndex)
        {
            selectedRoute.image.color = new Color(1f, 0.4f, 0.5f, 0.7f);
        }
        else if (currentColonyIndexPosition != uiRoute.firstColonyIndex && currentColonyIndexPosition == uiRoute.secondColonyIndex)
        {
            if (activeRoute != null) { activeRoute.image.color = new Color(0.3f, 1f, 0.6f, 0.7f); }
            selectedRoute.image.color = new Color(0.3f, 1f, 1f, 0.7f);
            activeRoute = selectedRoute;
        }
        else if (currentColonyIndexPosition == uiRoute.firstColonyIndex && currentColonyIndexPosition != uiRoute.secondColonyIndex)
        {
            if (activeRoute != null) { activeRoute.image.color = new Color(0.3f, 1f, 0.6f, 0.7f); }
            selectedRoute.image.color = new Color(0.3f, 1f, 1f, 0.7f);
            activeRoute = selectedRoute;
        }
        else if (currentColonyIndexPosition == uiRoute.firstColonyIndex && currentColonyIndexPosition == uiRoute.secondColonyIndex)
        {
            Debug.LogError($"One UIRoute got 2 equal colony index!!!!! ----- {selectedRoute.transform.position} -----"); return;
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
            uiFader.FadeIn();
            uiUserInterfaceMain.OnButtonLerpToDown();
            // VIAJAR AQUI
        }
    }
}