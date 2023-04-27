using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoute : MonoBehaviour
{
    [SerializeField] private Event spaceEvent; public Event GetSpaceEvent() { return spaceEvent; }
    [SerializeField] private sbyte firstColonyIndex; public sbyte GetFirstColonyIndex() { return firstColonyIndex; }
    [SerializeField] private sbyte secondColonyIndex; public sbyte GetSecondColonyIndex() { return secondColonyIndex; }
    [SerializeField] private Image image; public Image GetImage() { return image; } // Button image
    private void Awake() { image = this.GetComponentInChildren<Image>(); }
    public void OnButtonSelectRoute()
    {
        UIRouteSystem.Instance.UpdateSelectedRoute(this);
    }
}
