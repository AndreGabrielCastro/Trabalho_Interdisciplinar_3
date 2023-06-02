using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObjectAsteroid : EventObject
{
    [SerializeField] private EventAsteroidRing eventAsteroidRing;
    [SerializeField] private int speed = 1;
    [SerializeField] private int backSpeed = 4;
    [SerializeField] private int damage = 5;
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
        BeDestroyed();
    }
    public override void TakeDamage(int damage, Vector3 position)
    {
        Instantiate(eventAsteroidRing.GetVFXAsteroidHitted(), position, Quaternion.identity);
        Player.Instance.playerAudio.PlaySong(eventAsteroidRing.GetSFXAsteroidHitted());
        base.TakeDamage(damage, position);
    }
    public override void BeDestroyed()
    {
        Instantiate(eventAsteroidRing.GetVFXAsteroidDestroyed(), transform.position, Quaternion.identity);
        Player.Instance.playerAudio.PlaySong(eventAsteroidRing.GetSFXAsteroidDestroyed());
        base.BeDestroyed();
    }
}