using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxSystem : MonoBehaviour
{
    public static VfxSystem Instance;
    public GameObject vfxIsLate;
    public GameObject vfxDelivered;
    public GameObject vfxDeleted;
    public GameObject vfxInstantiated;

    private void Awake()
    {
        Instance = this;
    }
}
