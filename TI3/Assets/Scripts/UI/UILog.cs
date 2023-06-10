using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILog : MonoBehaviour
{
    public TMP_Text text;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    private void FixedUpdate()
    {
        GridObject gridObject = GridSystem.Instance.GetGridObject();
        if (gridObject == null) { text.text = "Hover over a grid cell and it's component's description will be shown here."; return; }
        text.text = gridObject.GetDescription();
    }
}
