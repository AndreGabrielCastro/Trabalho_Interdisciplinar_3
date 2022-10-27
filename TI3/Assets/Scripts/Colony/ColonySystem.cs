using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonySystem : MonoBehaviour
{
    public static ColonySystem Instance;

    [Header("Current Colony")]
    public int currentColonyIndexPosition;
    public string currentColonyName;

    [Header("Colony Related")]
    public Colony[] colonyArray;

    [Header("Task Related")]
    public Transform tasksContainer;
    public UITask uiTaskPrefab;
    public UITask[] uiTaskArray;

    [Header("Content Related")]
    public GridObject[] gridObjectDeliveryArray;

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
    private void OnLevelWasLoaded(int level)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("Colony") == true)
        {
            Debug.Log("Chegou à Colônia. -- Aqui caberia uma Action fácil -- André -- ColonySystemScript --");
        }
    }

    private void Start()
    {
        GenerateTasks(); // Generate tasks based on the current colony
    }
    public void GenerateTasks()
    {
        Colony currentColony = colonyArray[currentColonyIndexPosition];
        int taskAmount = Random.Range(currentColony.taskMinAmount, currentColony.taskMaxAmount + 1);
        this.uiTaskArray = new UITask[taskAmount];
        for (int i = 0; i < taskAmount; i++)
        {
            
            UITask uiTask = Instantiate(uiTaskPrefab, Vector3.zero, Quaternion.identity);
            uiTask.transform.SetParent(tasksContainer);
            uiTask.transform.localScale = Vector3.one;
            uiTask.SetTask(currentColony);
            //taskArray[i] = uiTask.GetComponent<Task>();
        }
    }
}