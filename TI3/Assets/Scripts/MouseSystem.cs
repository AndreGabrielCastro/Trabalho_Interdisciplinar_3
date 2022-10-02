using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSystem : MonoBehaviour
{
    public static MouseSystem Instance;
    [HideInInspector] public LayerMask mousePlaneLayerMask = new LayerMask();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Debug.LogError($" ----- There is more than one MouseSystem!!! ----- {this.transform} ----- {this.gameObject} -----");
            Destroy(this.gameObject);
            return;
        }
    }

    /// <summary>
    /// Returns the world position of the mouse projected into it's previously setted plane
    /// </summary>
    /// <returns></returns>
    public Vector3 GetWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, Instance.mousePlaneLayerMask);
        return raycastHit.point;
    }
}
