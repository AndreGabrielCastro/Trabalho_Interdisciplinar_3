using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxSystem : MonoBehaviour
{
    public static VfxSystem Instance;

    [Header("Space Ship Related")]
    public GameObject vfxSpaceShipDestroyed;
    [Header("Grid Object Related")]
    public GameObject vfxIsLate;
    public GameObject vfxDeleted;
    public GameObject vfxDestroyed;
    public GameObject vfxDelivered;
    public GameObject vfxInstantiated;
    [Header("Event Object Related")]
    public GameObject vfxEventObjectHitted;
    public GameObject vfxEventObjectDestroyed;

    private void Awake() { Instance = this; }
}
