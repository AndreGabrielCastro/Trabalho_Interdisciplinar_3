using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRouteSystem : MonoBehaviour
{
    public static UIRouteSystem Instance;
    public UIImageFader uiFader;

    [Header("Setted during playtime")]
    public UIRoute activeRoute;

    public sbyte currentColonyIndex;
    public sbyte destinationColonyIndex;

    private bool forceTravel;
    private float forceTimer = 5;

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
    private void Start() { Invoke(nameof(LateStart), 0.25f); }

    /// <summary>
    /// Called after 0.25 seconds.
    /// </summary>
    private void LateStart() { currentColonyIndex = ColonySystem.Instance.currentColonyIndex; }
    private void FixedUpdate()
    {
        if (forceTravel == true)
        {
            forceTimer -= Time.fixedDeltaTime;
            if (forceTimer <= 0) { forceTimer = 5; forceTravel = false; }
        }

        if (isLoading == false) { return; }
        timer -= Time.fixedDeltaTime;
        if (timer <= 0) { UnityEngine.SceneManagement.SceneManager.LoadScene("EventScene"); }
    }
    public void UpdateSelectedRoute(UIRoute uiRoute)
    {
        if (currentColonyIndex != uiRoute.GetFirstColonyIndex() && currentColonyIndex != uiRoute.GetSecondColonyIndex()) // If route is out of range...
        {
            uiRoute.GetImage().color = new Color(1f, 0.4f, 0.5f, 0.7f); // Sets the color of the route to red
        }
        else if (currentColonyIndex != uiRoute.GetFirstColonyIndex() && currentColonyIndex == uiRoute.GetSecondColonyIndex())
        {
            if (activeRoute != null) { activeRoute.GetImage().color = new Color(0.3f, 1f, 0.6f, 0.7f); } // Sets the color of the previous active route to green
            uiRoute.GetImage().color = new Color(0.3f, 1f, 1f, 0.7f); // Sets the color of the route to cyan
            activeRoute = uiRoute; // Sets the route as the new active route
            destinationColonyIndex = uiRoute.GetFirstColonyIndex(); // Stores the route's destination index
        }
        else if (currentColonyIndex == uiRoute.GetFirstColonyIndex() && currentColonyIndex != uiRoute.GetSecondColonyIndex())
        {
            if (activeRoute != null) { activeRoute.GetImage().color = new Color(0.3f, 1f, 0.6f, 0.7f); } // Sets the color of the previous active route to green
            uiRoute.GetImage().color = new Color(0.3f, 1f, 1f, 0.7f); // Sets the color of the route to cyan
            activeRoute = uiRoute; // Sets the route as the new active route
            destinationColonyIndex = uiRoute.GetSecondColonyIndex(); // Stores the route's destination index
        }
        else if (currentColonyIndex == uiRoute.GetFirstColonyIndex() && currentColonyIndex == uiRoute.GetSecondColonyIndex()) // If route has same indexes...
        {
            Debug.LogError($"One UIRoute got 2 equal colony index!!!!! ----- {uiRoute.transform.position} -----"); return;
        }
    }
    public bool IsRouteValid()
    {
        if (activeRoute == null)
        {
            UIUserInterface.Instance.PopResult("Select a valid route first!", Color.red); return false;
        }
        return true;
    }
    public bool IsTaskValid()
    {
        foreach (Task task in PlayerSystem.Instance.taskList)
        {
            for (int i = 0; i < task.gridObjectDeliveryArray.Length; i++)
            {
                if (task.gridObjectDeliveryArray[i].isPlaced == false)
                {
                    UIUserInterface.Instance.PopResult("Place all task's deliveries first", Color.red); return false;
                }
            }
        }
        return true;
    }
    public bool CheckMainComponents()
    {
        bool lacks = false;
        string warning = "You lack essential components such as:\n";

        bool lacksCannons = true;
        foreach (GridObject obj in PlayerSystem.Instance.GetMyGridObjects())
        { 
            if (obj.GetComponent<IFacilityOffensive>() != null) { lacksCannons = false; }
        }
        if (lacksCannons == true) { lacks = true; warning += " CANNON "; }

        if (Player.Instance.playerEnergy.GetEnergyGeneration() < 0.1f) { lacks = true; warning += " GENERATOR "; }

        if (Player.Instance.playerMovement.GetSpeed() < 0.1f) { lacks = true; warning += " ENGINE "; }

        if (lacks == true)
        {
            warning += "\nClick again to proceed.";
            UIUserInterface.Instance.PopResult(warning, Color.yellow, 10);
            return false;
        }
        return true;
    }
    public void OnButtonTryTravel()
    {
        if (IsRouteValid() == false) { return; }
        if (IsTaskValid() == false) { return; }

        if (forceTravel == true) { Travel(); }

        bool value = CheckMainComponents();
        if (value == true) { Travel(); }
        else if (value == false) { forceTravel = true; return; }
    }
    public void Travel()
    {
        UIUserInterface.Instance.OnButtonLerpToDown(); // Lerps the screen back to the SpaceShip
        UIUserInterface.Instance.uiFader.FadeIn(); // Activates the fade in
        isLoading = true; // Activates the timer
        PlayerSystem.Instance.SetCurrentColony(destinationColonyIndex); // Sets the current colony of the player
        Player.Instance.SetEvent(activeRoute.GetSpaceEvent());
        //PlayerSystem.Instance.isTravelling = true;
    }
}