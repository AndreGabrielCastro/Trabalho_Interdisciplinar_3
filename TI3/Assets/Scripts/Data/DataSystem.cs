using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSystem : MonoBehaviour
{
    private float time;
    private bool isFadingIn;
    private bool falseSave_TrueLoad;
    public void OnButtonSaveGame()
    {
        if (isFadingIn == true) { UIUserInterface.Instance.PopResult("Can't do this now!", Color.red); return; }

        falseSave_TrueLoad = false;
        isFadingIn = true;
        time = Time.time + 1.5f;
        UIUserInterface.Instance.uiFader.FadeIn();
    }
    public void OnButtonLoadGame()
    {
        if (isFadingIn == true) { UIUserInterface.Instance.PopResult("Can't do this now!", Color.red); return; }

        falseSave_TrueLoad = true;
        isFadingIn = true;
        time = Time.time + 1.5f;
        UIUserInterface.Instance.uiFader.FadeIn();
    }
    private void SaveGame()
    {
        GameData saveData = new GameData();
        saveData.playerData = new PlayerData();

        // PlayerData

        saveData.playerData.currentColonyIndex = PlayerSystem.Instance.currentColonyIndex;
        saveData.playerData.currentColonyName = PlayerSystem.Instance.currentColonyName;
        saveData.playerData.gearcoins = PlayerSystem.Instance.gearcoins;
        saveData.playerData.information = PlayerSystem.Instance.information;

        saveData.playerData.masterVolumeValue = PlayerSystem.Instance.masterVolumeValue;
        saveData.playerData.environmentVolumeValue = PlayerSystem.Instance.environmentVolumeValue;
        saveData.playerData.soundtrackVolumeValue = PlayerSystem.Instance.soundtrackVolumeValue;
        saveData.playerData.sfxVolumeValue = PlayerSystem.Instance.sfxVolumeValue;
        saveData.playerData.uiVolumeValue = PlayerSystem.Instance.uiVolumeValue;

        saveData.playerData.maximumIntegrity = Player.Instance.playerIntegrity.maximumIntegrity;
        saveData.playerData.currentIntegrity = Player.Instance.playerIntegrity.currentIntegrity;

        // GridObjectFacilityData

        int gridObjectFacilityAmount = 0;
        for (int i = 0; i < PlayerSystem.Instance.gridObjectList.Count; i++)
        {
            if (PlayerSystem.Instance.gridObjectList[i].TryGetComponent<GridObjectFacility>(out GridObjectFacility gridObjectFacility) == true)
            {
                gridObjectFacilityAmount++;
            }
        }

        int ii = 0;
        Debug.Log("F" + gridObjectFacilityAmount);
        saveData.gridObjectFacilityDataArray = new GridObjectFacilityData[gridObjectFacilityAmount];
        for (int i = 0; i < PlayerSystem.Instance.gridObjectList.Count; i++)
        {
            if (PlayerSystem.Instance.gridObjectList[i].TryGetComponent<GridObjectFacility>(out GridObjectFacility gridObjectFacility) == true)
            {
                saveData.gridObjectFacilityDataArray[ii] = new GridObjectFacilityData();
                saveData.gridObjectFacilityDataArray[ii].prefabIndex = PlayerSystem.Instance.gridObjectList[i].prefabIndex;

                saveData.gridObjectFacilityDataArray[ii].position = PlayerSystem.Instance.gridObjectList[i].transform.position;
                saveData.gridObjectFacilityDataArray[ii].rotation = PlayerSystem.Instance.gridObjectList[i].transform.eulerAngles;

                saveData.gridObjectFacilityDataArray[ii].desiredWidth = PlayerSystem.Instance.gridObjectList[i].desiredWidth;
                saveData.gridObjectFacilityDataArray[ii].desiredLength = PlayerSystem.Instance.gridObjectList[i].desiredLength;
                saveData.gridObjectFacilityDataArray[ii].snapValue = PlayerSystem.Instance.gridObjectList[i].snapValue;

                saveData.gridObjectFacilityDataArray[ii].maximumIntegrityPoints = PlayerSystem.Instance.gridObjectList[i].maximumIntegrityPoints;
                saveData.gridObjectFacilityDataArray[ii].currentIntegrityPoints = PlayerSystem.Instance.gridObjectList[ii].currentIntegrityPoints;

                ii++;
            }
        }

        // TaskData

        saveData.taskDataArray = new TaskData[PlayerSystem.Instance.taskList.Count];
        for (int i = 0; i < saveData.taskDataArray.Length; i++)
        {
            saveData.taskDataArray[i] = new TaskData();
            saveData.taskDataArray[i].origin = PlayerSystem.Instance.taskList[i].origin;
            saveData.taskDataArray[i].time = PlayerSystem.Instance.taskList[i].time;
            saveData.taskDataArray[i].destination = PlayerSystem.Instance.taskList[i].destination;
            saveData.taskDataArray[i].contentAmount = PlayerSystem.Instance.taskList[i].contentAmount;
            saveData.taskDataArray[i].gearcoinAmount = PlayerSystem.Instance.taskList[i].gearcoinAmount;
            saveData.taskDataArray[i].informationAmount = PlayerSystem.Instance.taskList[i].informationAmount;
            saveData.taskDataArray[i].contentDescription = PlayerSystem.Instance.taskList[i].contentDescription;
            saveData.taskDataArray[i].rewardDescription = PlayerSystem.Instance.taskList[i].rewardDescription;
            saveData.taskDataArray[i].isLate = PlayerSystem.Instance.taskList[i].isLate;

            // TaskData Deliveries

            saveData.taskDataArray[i].gridObjectDeliveryDataArray = new GridObjectDeliveryData[PlayerSystem.Instance.taskList[i].gridObjectDeliveryArray.Length];
            for (int j = 0; j < saveData.taskDataArray[i].gridObjectDeliveryDataArray.Length; j++)
            {
                saveData.taskDataArray[i].gridObjectDeliveryDataArray[j] = new GridObjectDeliveryData();
                saveData.taskDataArray[i].gridObjectDeliveryDataArray[j].prefabIndex = PlayerSystem.Instance.taskList[i].gridObjectDeliveryArray[j].prefabIndex;

                saveData.taskDataArray[i].gridObjectDeliveryDataArray[j].position = PlayerSystem.Instance.taskList[i].gridObjectDeliveryArray[j].transform.position;
                saveData.taskDataArray[i].gridObjectDeliveryDataArray[j].rotation = PlayerSystem.Instance.taskList[i].gridObjectDeliveryArray[j].transform.eulerAngles;

                saveData.taskDataArray[i].gridObjectDeliveryDataArray[j].desiredWidth = PlayerSystem.Instance.taskList[i].gridObjectDeliveryArray[j].desiredWidth;
                saveData.taskDataArray[i].gridObjectDeliveryDataArray[j].desiredLength = PlayerSystem.Instance.taskList[i].gridObjectDeliveryArray[j].desiredLength;
                saveData.taskDataArray[i].gridObjectDeliveryDataArray[j].snapValue = PlayerSystem.Instance.taskList[i].gridObjectDeliveryArray[j].snapValue;

                saveData.taskDataArray[i].gridObjectDeliveryDataArray[j].maximumIntegrityPoints = PlayerSystem.Instance.taskList[i].gridObjectDeliveryArray[j].maximumIntegrityPoints;
                saveData.taskDataArray[i].gridObjectDeliveryDataArray[j].currentIntegrityPoints = PlayerSystem.Instance.taskList[i].gridObjectDeliveryArray[j].currentIntegrityPoints;
            }
        }

        string save = JsonUtility.ToJson(saveData);
        string path = Application.dataPath + "/save.txt";
        File.WriteAllText(path, save);

        UIUserInterface.Instance.PopResult("Game Saved Successfully", Color.green, 4);
    }
    private void LoadGame()
    {
        string load = File.ReadAllText(Application.dataPath + "/save.txt");
        GameData loadData = JsonUtility.FromJson<GameData>(load);

        // PlayerData

        PlayerSystem.Instance.currentColonyIndex = loadData.playerData.currentColonyIndex;
        PlayerSystem.Instance.currentColonyName = loadData.playerData.currentColonyName;
        PlayerSystem.Instance.gearcoins = loadData.playerData.gearcoins;
        PlayerSystem.Instance.information = loadData.playerData.information;

        PlayerSystem.Instance.masterVolumeValue = loadData.playerData.masterVolumeValue;
        PlayerSystem.Instance.environmentVolumeValue = loadData.playerData.environmentVolumeValue;
        PlayerSystem.Instance.soundtrackVolumeValue = loadData.playerData.soundtrackVolumeValue;
        PlayerSystem.Instance.sfxVolumeValue = loadData.playerData.sfxVolumeValue;
        PlayerSystem.Instance.uiVolumeValue = loadData.playerData.uiVolumeValue;

        Player.Instance.playerIntegrity.maximumIntegrity = loadData.playerData.maximumIntegrity;
        Player.Instance.playerIntegrity.currentIntegrity = loadData.playerData.currentIntegrity;

        // GridObjectFacilityData

        // Excluding current data

        for (int i = 0; i < PlayerSystem.Instance.gridObjectList.Count; i++)
        {
            Destroy(PlayerSystem.Instance.gridObjectList[i].gameObject);
        }
        PlayerSystem.Instance.gridObjectList = new List<GridObject>();

        // Creating new data

        for (int i = 0; i < loadData.gridObjectFacilityDataArray.Length; i++)
        {
            GridObjectFacility gridObjectFacility = Instantiate(SpaceShipSystem.Instance.allGridObjectFacilityPrefabArray[loadData.gridObjectFacilityDataArray[i].prefabIndex],
                                                                loadData.gridObjectFacilityDataArray[i].position,
                                                                Quaternion.Euler(loadData.gridObjectFacilityDataArray[i].rotation));

            gridObjectFacility.desiredWidth = loadData.gridObjectFacilityDataArray[i].desiredWidth;
            gridObjectFacility.desiredLength = loadData.gridObjectFacilityDataArray[i].desiredLength;
            gridObjectFacility.snapValue = loadData.gridObjectFacilityDataArray[i].snapValue;

            GridEdgePosition gridFloatPosition = new GridEdgePosition();

            if (gridObjectFacility.snapValue == 0)
            { gridFloatPosition = GridSystem.Instance.GetGridGroundFloatPosition(gridObjectFacility.transform.position); } // Gets the ground grid position from the world position
            else if (gridObjectFacility.snapValue == 1)
            { gridFloatPosition = GridSystem.Instance.GetGridEdgeHorizontalPosition(gridObjectFacility.transform.position); } // Gets the horizontal grid edge position from the world position
            else if (gridObjectFacility.snapValue == 2)
            { gridFloatPosition = GridSystem.Instance.GetGridEdgeVerticalPosition(gridObjectFacility.transform.position); } // Gets the vertical grid edge position from the world position
            else if (gridObjectFacility.snapValue == 3)
            { gridFloatPosition = GridSystem.Instance.GetGridEdgeCornerPosition(gridObjectFacility.transform.position); } // Gets the corner grid edge position from the world position

            gridFloatPosition -= new GridEdgePosition(((float)gridObjectFacility.desiredWidth - 1) / 2, ((float)gridObjectFacility.desiredLength - 1) / 2);
            GridPosition gridPosition = new GridPosition(gridFloatPosition);

            int arrayPosition = 0;
            for (int x = 0; x < gridObjectFacility.desiredWidth; x++) // Goes through each line
            {
                for (int z = 0; z < gridObjectFacility.desiredLength; z++) // Goes through each column
                {
                    GridTile gridTile = GridSystem.Instance.TryGetGridTile(gridPosition + new GridPosition(x, z)); // Try to get the grid tile from the grid position considering line and column
                    gridTile.SetGridObject(gridObjectFacility); // Sets the grid object to the grid tile and updates the grid visual
                    gridObjectFacility.SetGridTile(gridTile, arrayPosition); // Sets the grid tiles the grid object occupies (Mainly used to easily delete the grid object later)
                    arrayPosition += 1; // Increases the array position
                }
            }

            gridObjectFacility.maximumIntegrityPoints = loadData.gridObjectFacilityDataArray[i].maximumIntegrityPoints;
            gridObjectFacility.currentIntegrityPoints = loadData.gridObjectFacilityDataArray[i].currentIntegrityPoints;

            gridObjectFacility.transform.SetParent(GridSystem.Instance.transform);
            PlayerSystem.Instance.gridObjectList.Add(gridObjectFacility);
        }

        // TaskData

        // Excluding current data

        for (int i = 0; i < PlayerSystem.Instance.taskList.Count; i++)
        {
            PlayerSystem.Instance.taskList[i] = null;
        }
        PlayerSystem.Instance.taskList = new List<Task>();

        // Creating new data

        for (int i = 0; i < loadData.taskDataArray.Length; i++)
        {
            Task task = new Task();
            task.origin = loadData.taskDataArray[i].origin;
            task.time = loadData.taskDataArray[i].time;
            task.destination = loadData.taskDataArray[i].destination;
            task.contentAmount = loadData.taskDataArray[i].contentAmount;
            task.gearcoinAmount = loadData.taskDataArray[i].gearcoinAmount;
            task.informationAmount = loadData.taskDataArray[i].informationAmount;
            task.contentDescription = loadData.taskDataArray[i].contentDescription;
            task.rewardDescription = loadData.taskDataArray[i].rewardDescription;
            task.isLate = loadData.taskDataArray[i].isLate;

            task.gridObjectDeliveryArray = new GridObjectDelivery[loadData.taskDataArray[i].gridObjectDeliveryDataArray.Length];
            for (int j = 0; j < loadData.taskDataArray[i].gridObjectDeliveryDataArray.Length; j++)
            {
                GridObjectDelivery gridObjectDelivery = Instantiate(UITaskMenuSystem.Instance.allGridObjectDeliveryPrefabArray[loadData.taskDataArray[i].gridObjectDeliveryDataArray[j].prefabIndex],
                                                                    loadData.taskDataArray[i].gridObjectDeliveryDataArray[j].position,
                                                                    Quaternion.Euler(loadData.taskDataArray[i].gridObjectDeliveryDataArray[j].rotation));

                gridObjectDelivery.desiredWidth = loadData.taskDataArray[i].gridObjectDeliveryDataArray[j].desiredWidth;
                gridObjectDelivery.desiredLength = loadData.taskDataArray[i].gridObjectDeliveryDataArray[j].desiredLength;
                gridObjectDelivery.snapValue = loadData.taskDataArray[i].gridObjectDeliveryDataArray[j].snapValue;

                GridEdgePosition gridFloatPosition = new GridEdgePosition();

                if (gridObjectDelivery.snapValue == 0)
                { gridFloatPosition = GridSystem.Instance.GetGridGroundFloatPosition(gridObjectDelivery.transform.position); } // Gets the ground grid position from the world position
                else if (gridObjectDelivery.snapValue == 1)
                { gridFloatPosition = GridSystem.Instance.GetGridEdgeHorizontalPosition(gridObjectDelivery.transform.position); } // Gets the horizontal grid edge position from the world position
                else if (gridObjectDelivery.snapValue == 2)
                { gridFloatPosition = GridSystem.Instance.GetGridEdgeVerticalPosition(gridObjectDelivery.transform.position); } // Gets the vertical grid edge position from the world position
                else if (gridObjectDelivery.snapValue == 3)
                { gridFloatPosition = GridSystem.Instance.GetGridEdgeCornerPosition(gridObjectDelivery.transform.position); } // Gets the corner grid edge position from the world position

                gridFloatPosition -= new GridEdgePosition(((float)gridObjectDelivery.desiredWidth - 1) / 2, ((float)gridObjectDelivery.desiredLength - 1) / 2);
                GridPosition gridPosition = new GridPosition(gridFloatPosition);

                int arrayPosition = 0;
                for (int x = 0; x < gridObjectDelivery.desiredWidth; x++) // Goes through each line
                {
                    for (int z = 0; z < gridObjectDelivery.desiredLength; z++) // Goes through each column
                    {
                        GridTile gridTile = GridSystem.Instance.TryGetGridTile(gridPosition + new GridPosition(x, z)); // Try to get the grid tile from the grid position considering line and column
                        gridTile.SetGridObject(gridObjectDelivery); // Sets the grid object to the grid tile and updates the grid visual
                        gridObjectDelivery.SetGridTile(gridTile, arrayPosition); // Sets the grid tiles the grid object occupies (Mainly used to easily delete the grid object later)
                        arrayPosition += 1; // Increases the array position
                    }
                }

                gridObjectDelivery.maximumIntegrityPoints = loadData.taskDataArray[i].gridObjectDeliveryDataArray[j].maximumIntegrityPoints;
                gridObjectDelivery.currentIntegrityPoints = loadData.taskDataArray[i].gridObjectDeliveryDataArray[j].currentIntegrityPoints;

                gridObjectDelivery.isPlaced = true;
                gridObjectDelivery.transform.SetParent(GridSystem.Instance.transform);
                task.gridObjectDeliveryArray[j] = gridObjectDelivery;
            }

            PlayerSystem.Instance.taskList.Add(task);
        }

        UIRouteSystem.Instance.currentColonyIndex = loadData.playerData.currentColonyIndex;
        UIUserInterface.Instance.PopResult("Game Loaded Successfully", Color.green, 4);
    }
    private void FixedUpdate()
    {
        if (isFadingIn == false) { return; }
        if (time >= Time.time) { return; }

        if (falseSave_TrueLoad == false) { SaveGame(); }
        else if (falseSave_TrueLoad == true) { LoadGame(); }

        isFadingIn = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("ColonyScene");
    }
}