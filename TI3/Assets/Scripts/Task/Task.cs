using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Task
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
    public GridObjectDelivery[] gridObjectDeliveryArray;
    public UIGridObjectDelivery[] uiGridObjectDeliveryArray;

    public Task() { }
    public Task(string origin, int time, string destination, int contentAmount,
                int gearcoinAmount, int informationAmount,
                string contentDescription, string rewardDescription,
                GridObjectDelivery[] gridObjectDeliveryArray, UIGridObjectDelivery[] uiGridObjectDeliveryArray)
    {
        this.origin = origin;
        this.time = time;
        this.destination = destination;
        this.contentAmount = contentAmount;
        this.gearcoinAmount = gearcoinAmount;
        this.informationAmount = informationAmount;
        this.contentDescription = contentDescription;
        this.rewardDescription = rewardDescription;

        int amount = gridObjectDeliveryArray.Length;
        this.gridObjectDeliveryArray = new GridObjectDelivery[amount];
        this.uiGridObjectDeliveryArray = new UIGridObjectDelivery[amount];

        for (int i = 0; i < amount; i++)
        {
            this.gridObjectDeliveryArray[i] = gridObjectDeliveryArray[i];
            this.uiGridObjectDeliveryArray[i] = uiGridObjectDeliveryArray[i];
        }
    }
}