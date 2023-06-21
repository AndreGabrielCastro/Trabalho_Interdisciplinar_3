using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIWorkerCard : MonoBehaviour
{
    [SerializeField] private Worker worker;
    [Header("Setted during playtime")]
    [SerializeField] private Image workerIcon;
    [SerializeField] private int gearcoinPrice;
    public void OnButtonTryCreate()
    {
        if (PlayerSystem.Instance.gearcoins < gearcoinPrice)
        { UIUserInterface.Instance.PopResult("Not enough gearcoins!", Color.red); return; }
        PlayerSystem.Instance.AlterateWorkersStored(+1);
        PlayerSystem.Instance.AlterateGearcoins(-gearcoinPrice);
        UIUserInterface.Instance.PopResult("Facility sucessfully created!", Color.green);
    }
}
