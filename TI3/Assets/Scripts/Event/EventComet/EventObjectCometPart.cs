using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObjectCometPart : EventObject
{
    public GameObject[] partMeshArray;
    private EventComet eventComet;
    private EventObjectCometCore cometCore;
    public void SetAttributes(EventComet eventComet, EventObjectCometCore cometCore)
    {
        this.eventComet = eventComet;
        this.cometCore = cometCore;
        this.integrityPoints = eventComet.GetPartsIntegrity();
    }
    public void ThrowShard()
    {
        EventObjectCometShard shard = Instantiate(eventComet.GetShardPrefab(), transform.position, Quaternion.identity);
        shard.SetAttributes(eventComet);
    }
    public override void TakeDamage(int damage, Vector3 position)
    {
        Instantiate(eventComet.GetVFXHitted(), position, Quaternion.identity);
        Player.Instance.playerAudio.PlaySong(eventComet.GetSFXHitted());
        base.TakeDamage(damage, position);

        int value = Random.Range(0, 101);
        if (value <= eventComet.GetShardChance())
        {
            ThrowShard();
        }
    }
    public override void BeDestroyed()
    {
        Instantiate(eventComet.GetVFXPartExplosion(), transform.position, Quaternion.identity);
        Player.Instance.playerAudio.PlaySong(eventComet.GetSFXPartExplosion());
        cometCore.TakeDamage(eventComet.GetPartsIntegrity() * 2);
        foreach (GameObject partMesh in partMeshArray)
        {
            Destroy(partMesh);
        }
        base.BeDestroyed();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<GridObject>(out GridObject gridObject) == true)
        {
            gridObject.TakeDamage((int)((float)eventComet.GetPartsDamage() * 0.5f));
            Player.Instance.playerIntegrity.TakeDamage((int)((float)eventComet.GetPartsDamage() * 0.5f));
            Player.Instance.transform.position += Vector3.forward * 1;
        }
        else if (collision.transform.TryGetComponent<Player>(out Player player) == true)
        {
            Player.Instance.playerIntegrity.TakeDamage(eventComet.GetPartsDamage());
            Player.Instance.transform.position += Vector3.forward * 1;
        }
    }
}