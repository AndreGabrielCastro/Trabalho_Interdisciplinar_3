using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventComet : Event
{
    public EventObjectComet eventObjectCometPrefab;
    public EventObjectCometShard eventObjectCometShardPrefab;
    public int cometPartsIntegrity;
    public int cometPartsDamage;
    public float cometMovementSpeed;
    public float cometRotationSpeed;
    public int cometShardChancePerDamage;
    public int cometShardIntegrity;
    public int cometShardDamage;
    public int cometShardSpeed;
    private void Start()
    {
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