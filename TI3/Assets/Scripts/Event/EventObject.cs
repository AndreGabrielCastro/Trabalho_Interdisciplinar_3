using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObject : MonoBehaviour
{
    [Header("Must be setted")]
    public int integrityPoints = 20;
    [Header("Setted during playtime")]
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
    public virtual void TakeDamage(int damage)
    {
        if (integrityPoints <= 0) { return; }
        integrityPoints -= damage;
        if (integrityPoints <= 0)
        {
            BeDestroyed();
        }
    }
    public virtual void TakeDamage(int damage, Vector3 position)
    {
        TakeDamage(damage);
    }
    public virtual void BeDestroyed()
    {
        Destroy(this.gameObject);
    }
}