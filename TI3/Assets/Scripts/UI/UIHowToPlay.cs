using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHowToPlay : MonoBehaviour
{
    public GameObject[] UIHelpGameObjectsArray;
    public void OnButtonEnableHelp()
    { for (int i = 0; i < UIHelpGameObjectsArray.Length; i++) { UIHelpGameObjectsArray[i].SetActive(true); } }
}
