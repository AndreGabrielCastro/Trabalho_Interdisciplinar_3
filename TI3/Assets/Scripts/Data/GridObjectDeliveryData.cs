using System;
using UnityEngine;

[Serializable]
public class GridObjectDeliveryData
{
    public int prefabIndex;
    public Vector3 position;
    public Vector3 rotation;
    public int desiredWidth;
    public int desiredLength;
    public int snapValue;
    public int maximumIntegrityPoints;
    public float currentIntegrityPoints;
}