using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Task
{
    public string origin;
    public string destination;
    public int contentAmount;
    public int gearcoinAmount;
    public int informationAmount;
    public string description;
    public GridObjectDelivery[] gridObjectDeliveryArray;
    public UIGridObjectDelivery[] uiGridObjectDeliveryArray;

    public Task(string origin, string destination, int contentAmount,
                int gearcoinAmount, int informationAmount, string description,
                GridObjectDelivery[] gridObjectDeliveryArray, UIGridObjectDelivery[] uiGridObjectDeliveryArray)
    {
        this.origin = origin;
        this.destination = destination;
        this.contentAmount = contentAmount;
        this.gearcoinAmount = gearcoinAmount;
        this.informationAmount = informationAmount;
        this.description = description;
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