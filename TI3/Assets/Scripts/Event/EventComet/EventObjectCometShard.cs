using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObjectCometShard : EventObject
{
    private EventComet eventComet;
    public int speed = 1;
    private GridPosition currentGridPosition;
    private Transform meshTransform;
    private Vector3 rotationDirection;
    public void SetAttributes(EventComet eventComet)
    {
        this.eventComet = eventComet;
        this.integrityPoints = eventComet.cometShardIntegrity;

        Vector3 direction = (Player.Instance.transform.position + new Vector3(Random.Range(-15, 15), 0, 0)) - transform.position;
        transform.forward = direction.normalized;
    }
    private void Start()
    {
        meshTransform = this.transform.Find("Mesh");
        rotationDirection = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), Random.Range(-30, 30));
    }
    private void FixedUpdate()
    {
        if (this.transform.position.z <= -25) { Destroy(this.gameObject); }

        this.transform.Translate(Vector3.forward * eventComet.cometShardSpeed * Time.fixedDeltaTime);
        meshTransform.Rotate(rotationDirection * 1 * Time.fixedDeltaTime);

        if (this.isActive == false) { return; }
        GridPosition gridPosition = GridSystem.Instance.GetGridGroundPositionRelative(this.transform.position);
        if (gridPosition == currentGridPosition) { return; }
        currentGridPosition = gridPosition;
        GridTile gridTile = GridSystem.Instance.TryGetGridTile(gridPosition);
        if (gridTile == null) { return; }
        gridTile.TakeDamage(eventComet.cometShardDamage);
        Player.Instance.playerIntegrity.TakeDamage(eventComet.cometShardDamage);
        Instantiate(VfxSystem.Instance.vfxEventObjectDestroyed, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
