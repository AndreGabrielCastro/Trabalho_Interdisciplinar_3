using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColony : MonoBehaviour
{
    [HideInInspector] public Image image;
    private void Awake()
    {
        this.image = this.GetComponentInChildren<Image>();
    }
    public void OnButtonSelectColony()
    {
        UIUserInterface.Instance.PopResult("Can't do this yet", Color.red);
    }
}
