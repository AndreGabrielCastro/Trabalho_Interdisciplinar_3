using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoute : MonoBehaviour
{
    public int firstColonyIndex;
    public int secondColonyIndex;
    [HideInInspector] public Image image; // Button image
    private void Awake() { image = this.GetComponentInChildren<Image>(); }
    public void OnButtonSelectRoute()
    {
        UIRouteSystem.Instance.UpdateSelectedRoute(this);
    }
}
