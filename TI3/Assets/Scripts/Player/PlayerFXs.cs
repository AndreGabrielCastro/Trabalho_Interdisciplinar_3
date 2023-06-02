using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFXs : MonoBehaviour
{
    [Header("Space Ship Related")]
    [SerializeField] private GameObject vfxSpaceShipDestroyed; public GameObject GetVFXSpaceShipDestroyed() { return vfxSpaceShipDestroyed; }
    [SerializeField] private AudioClip sfxSpaceShipDestroyed; public AudioClip GetSFXSpaceShipDestroyed() { return sfxSpaceShipDestroyed; }
    [Header("Grid Object Related")]
    [SerializeField] private GameObject vfxGridObjectInstantiated; public GameObject GetVFXGridObjectInstantiated() { return vfxGridObjectInstantiated; }
    [SerializeField] private GameObject vfxGridObjectDeleted; public GameObject GetVFXGridObjectDeleted() { return vfxGridObjectDeleted; }
    [SerializeField] private GameObject vfxGridObjectDestroyed; public GameObject GetVFXGridObjectDestroyed() { return vfxGridObjectDestroyed; }
    [SerializeField] private GameObject vfxDeliveryDelivered; public GameObject GetVFXDeliveryDelivered() { return vfxDeliveryDelivered; }
    [SerializeField] private GameObject vfxDeliveryLate; public GameObject GetVFXDeliveryLate() { return vfxDeliveryLate; }
    [SerializeField] private AudioClip sfxGridObjectInstantiated; public AudioClip GetSFXGridObjectInstantiated() { return sfxGridObjectInstantiated; }
    [SerializeField] private AudioClip sfxGridObjectDeleted; public AudioClip GetSFXGridObjectDeleted() { return sfxGridObjectDeleted; }
    [SerializeField] private AudioClip sfxGridObjectDestroyed; public AudioClip GetSFXGridObjectDestroyed() { return sfxGridObjectDestroyed; }
    [SerializeField] private AudioClip sfxDeliveryDelivered; public AudioClip GetSFXDeliveryDelivered() { return sfxDeliveryDelivered; }
    [SerializeField] private AudioClip sfxDeliveryLate; public AudioClip GetSFXDeliveryLate() { return sfxDeliveryLate; }
}