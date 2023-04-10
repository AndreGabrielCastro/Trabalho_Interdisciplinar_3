using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public PlayerData playerData;
    public GridObjectFacilityData[] gridObjectFacilityDataArray;
    public TaskData[] taskDataArray;
    public WorkerData[] workerDataArray;
}