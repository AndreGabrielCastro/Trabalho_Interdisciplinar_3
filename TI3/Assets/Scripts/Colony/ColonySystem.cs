using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColonySystem : MonoBehaviour
{
    public static ColonySystem Instance;

    [Header("Current Colony")]
    public int currentColonyIndex;
    public string currentColonyName;

    [Header("Colony Structure")]
    public TMP_Text colonyNameText;

    [Header("All Colonies Related")]
    public Colony[] allColoniesArray;
    public UIColony[] allUIColoniesArray; // Each of these means the image representation of the colonies above in the galaxy map

    private void Awake()
    {
        #region ErrorTreatment
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Debug.LogError($" ----- There is more than one ColonySystem!!! ----- {this.transform.position} ----- {this.gameObject} -----");
            Destroy(this.gameObject);
            return;
        }
        #endregion
    }

    /// <summary>
    /// Updates the all things related to the colony based on the index received.
    /// </summary>
    /// <param name="colonyIndex"></param>
    public void UpdateCurrentColony(int colonyIndex) 
    {
        this.currentColonyIndex = colonyIndex;
        this.currentColonyName = allColoniesArray[colonyIndex].colonyName;
        this.colonyNameText.text = currentColonyName;

        allUIColoniesArray[colonyIndex].image.color = new Color(0.3f, 1f, 1f, 1f); // Sets the color of the route to cyan

        UITaskMenuSystem.Instance.GenerateUITasks(); // Generate tasks based on the current colony
    }
}