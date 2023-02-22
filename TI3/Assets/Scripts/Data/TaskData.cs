using System;
using UnityEngine;

[Serializable]
public class TaskData
{
    public string origin;
    public int time;
    public string destination;
    public int contentAmount;
    public int gearcoinAmount;
    public int informationAmount;
    public string contentDescription;
    public string rewardDescription;
    public bool isLate = false;
    public GridObjectDeliveryData[] gridObjectDeliveryDataArray;
}