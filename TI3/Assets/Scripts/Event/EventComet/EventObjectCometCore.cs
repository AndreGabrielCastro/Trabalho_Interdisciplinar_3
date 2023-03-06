﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObjectCometCore : EventObject
{
    public GameObject coreMesh;
    private EventComet eventComet;
    private EventObjectComet objectComet;
    public void SetAttributes(EventComet eventComet, EventObjectComet objectComet)
    {
        this.eventComet = eventComet;
        this.objectComet = objectComet;
        this.integrityPoints = eventComet.cometPartsIntegrity * (8 + 4) * 2;
    }
    public void ThrowShard()
    {
        EventObjectCometShard shard = Instantiate(eventComet.eventObjectCometShardPrefab, transform.position, Quaternion.identity);
        shard.SetAttributes(eventComet);
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        int value = Random.Range(0, 101);
        if (value <= eventComet.cometShardChancePerDamage)
        {
            ThrowShard();
        }

        objectComet.transform.position -= Vector3.forward * 0.25f;
    }
    public override void BeDestroyed()
    {
        objectComet.TakeDamage(objectComet.integrityPoints);
        Destroy(coreMesh);
        base.BeDestroyed();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<GridObject>(out GridObject gridObject) == true)
        {
            gridObject.TakeDamage(eventComet.cometPartsDamage);
            Player.Instance.transform.position += Vector3.forward * 0.5f;
        }
    }
}