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
    public UIColony[] allUIColoniesArray; // Each of there means the image representation of the colonies above in the galaxy map

    [Header("UI Task Related")]
    public Transform tasksContainer;
    public UITask uiTaskPrefab;
    public UITask[] uiTaskArray;

    [Header("All Contents Related")]
    public GridObject[] allGridObjectDeliveriesArray;

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
    public void UpdateCurrentColony(int colonyIndex) 
    {
        this.currentColonyIndex = colonyIndex;
        this.currentColonyName = allColoniesArray[colonyIndex].colonyName;
        this.colonyNameText.text = currentColonyName;

        allUIColoniesArray[colonyIndex].image.color = new Color(0.3f, 1f, 1f, 1f); // Sets the color of the route to cyan

        GenerateTasks(); // Generate tasks based on the current colony
    }
    public void GenerateTasks()
    {
        Colony currentColony = allColoniesArray[currentColonyIndex]; // Gets the current colony from the All Colonies Array
        int taskAmount = Random.Range(currentColony.taskMinAmount, currentColony.taskMaxAmount + 1); // Determines the new size of the UI task array
        this.uiTaskArray = new UITask[taskAmount]; // Sets the new size of the UI task array

        Debug.Log(taskAmount);

        for (int i = 0; i < taskAmount; i++)
        {
            UITask uiTask = Instantiate(uiTaskPrefab, Vector3.zero, Quaternion.identity); // Creates the UI task
            uiTask.transform.SetParent(tasksContainer); // Sets it as child of the UI task container
            uiTask.transform.localScale = Vector3.one; // Sets it's scale to 1
            uiTask.SetTask(currentColony); // Sets the atributes to the UI task
        }
    }
}