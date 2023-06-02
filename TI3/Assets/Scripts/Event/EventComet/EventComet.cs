using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventComet : Event
{

    [SerializeField] private EventObjectComet eventObjectCometPrefab;
    [SerializeField] private EventObjectCometShard eventObjectCometShardPrefab; public EventObjectCometShard GetShardPrefab() { return eventObjectCometShardPrefab; }
    [SerializeField] private GameObject vfxEventCometCoreExplosion; public GameObject GetVFXCoreExplosion() { return vfxEventCometCoreExplosion; }
    [SerializeField] private GameObject vfxEventCometPartExplosion; public GameObject GetVFXPartExplosion() { return vfxEventCometPartExplosion; }
    [SerializeField] private GameObject vfxEventCometShardExplosion; public GameObject GetVFXShardExplosion() { return vfxEventCometShardExplosion; }
    [SerializeField] private GameObject vfxEventCometHitted; public GameObject GetVFXHitted() { return vfxEventCometHitted; }
    [SerializeField] private AudioClip sfxEventCometCoreExplosion; public AudioClip GetSFXCoreExplosion() { return sfxEventCometCoreExplosion; }
    [SerializeField] private AudioClip sfxEventCometPartExplosion; public AudioClip GetSFXPartExplosion() { return sfxEventCometPartExplosion; }
    [SerializeField] private AudioClip sfxEventCometShardExplosion; public AudioClip GetSFXShardExplosion() { return sfxEventCometShardExplosion; }
    [SerializeField] private AudioClip sfxEventCometHitted; public AudioClip GetSFXHitted() { return sfxEventCometHitted; }
    [SerializeField] private int cometPartsIntegrity; public int GetPartsIntegrity() { return cometPartsIntegrity; }
    [SerializeField] private int cometPartsDamage; public int GetPartsDamage() { return cometPartsDamage; }
    [SerializeField] private float cometMovementSpeed; public float GetMovementSpeed() { return cometMovementSpeed; }
    [SerializeField] private float cometRotationSpeed; public float GetRotationSpeed() { return cometRotationSpeed; }
    [SerializeField] private int cometShardChancePerDamage; public int GetShardChance() { return cometShardChancePerDamage; }
    [SerializeField] private int cometShardIntegrity; public int GetShardIntegrity() { return cometShardIntegrity; }
    [SerializeField] private int cometShardDamage; public int GetShardDamage() { return cometShardDamage; }
    [SerializeField] private int cometShardSpeed; public int GetShardSpeed() { return cometShardSpeed; }
    private void Start()
    {
        PlaySoundTrack();

        EventObjectComet eventObjectComet = Instantiate(eventObjectCometPrefab, new Vector3(0, 0, -30), Quaternion.identity);
        eventObjectComet.SetAttributes(this, cometMovementSpeed, cometRotationSpeed);

        EventObjectCometCore eventObjectCometCore = eventObjectComet.GetComponentInChildren<EventObjectCometCore>();
        eventObjectCometCore.SetAttributes(this, eventObjectComet);
                                                                                  // comets + core + doubled

        EventObjectCometPart[] eventObjectCometPartArray = eventObjectComet.GetComponentsInChildren<EventObjectCometPart>();
        foreach (EventObjectCometPart eventObjectCometPart in eventObjectCometPartArray)
        {
            eventObjectCometPart.SetAttributes(this, eventObjectCometCore);
        }
    }
}