using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatSystem : MonoBehaviour
{
    public void CheatAddGearcoin(int value)
    {
        PlayerSystem.Instance.AlterateGearcoins(value);
    }
    public void CheatAddInformation(int value)
    {
        PlayerSystem.Instance.AlterateInformation(value);
    }
}
