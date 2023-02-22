using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public sbyte currentColonyIndex;
    public string currentColonyName;

    public int gearcoins;
    public byte workingEngineer;
    public byte standbyEngineer;
    public int information;

    public sbyte masterVolumeValue = 0;
    public sbyte environmentVolumeValue = 0;
    public sbyte soundtrackVolumeValue = 0;
    public sbyte sfxVolumeValue = 0;
    public sbyte uiVolumeValue = 0;

    public int maximumIntegrity;
    public int currentIntegrity;
}