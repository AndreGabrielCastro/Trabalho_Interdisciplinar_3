using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObject : MonoBehaviour
{
    public bool isActive;
    public void SetActive()
    {
        if (isActive == true) { return; }
        isActive = true;
        this.transform.gameObject.layer = Player.Instance.activeEventObjectLayerMask;
    }
}
