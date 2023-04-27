using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHowToPlay : MonoBehaviour
{
    public static UIHowToPlay Instance;
    private List<UIHint> uiHintList = new List<UIHint>();
    public void AddUIHint(UIHint hint) { uiHintList.Add(hint); }
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }
    public void OnButtonEnableHelp()
    { 
        foreach(UIHint hint in uiHintList)
        {
            hint.gameObject.SetActive(true);
        }
    }
}