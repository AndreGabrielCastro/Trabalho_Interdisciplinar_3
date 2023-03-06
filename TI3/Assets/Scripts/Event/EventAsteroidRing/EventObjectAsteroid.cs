using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObjectAsteroid : EventObject
{
    public int speed = 1;
    public int backSpeed = 4;
    public int damage = 5;
    private GridPosition currentGridPosition;
    private Transform meshTransform;
    private Vector3 rotationDirection;
    private void Start()
    { 
        meshTransform = this.transform.Find("Mesh");
        rotationDirection = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), Random.Range(-30, 30));
    }
    private void FixedUpdate()
    {
        if (this.transform.position.z <= -25) { Destroy(this.gameObject); }

        this.transform.position += Vector3.back * backSpeed * Time.fixedDeltaTime;
        this.transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
        meshTransform.Rotate(rotationDirection * 1 * Time.fixedDeltaTime);

        if (this.isActive == false) { return; }
        GridPosition gridPosition = GridSystem.Instance.GetGridGroundPositionRelative(this.transform.position);
        if (gridPosition == currentGridPosition) { return; }
        currentGridPosition = gridPosition;
        GridTile gridTile = GridSystem.Instance.TryGetGridTile(gridPosition);
        if (gridTile == null) { return; }
        gridTile.TakeDamage(damage);
        Player.Instance.playerIntegrity.TakeDamage(damage);
        Instantiate(VfxSystem.Instance.vfxEventObjectDestroyed, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
