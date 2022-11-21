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
        this.gameObject.layer = Player.Instance.activeEventObjectLayerMaskValue;

        // layerMask.value returns a value bigger than 32.
        // LayerMask.NameToLayer(layerMask.ToString()) returns -1
        // I don't know why Unity does not have support for this kind of thing...
    }
}
