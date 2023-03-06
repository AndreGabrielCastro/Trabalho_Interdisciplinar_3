using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObjectComet : EventObject
{
    private EventComet eventComet;
    [SerializeField] private float movementSpeed = 0.1f;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private Vector3 rotationDirection;
    public void SetAttributes(EventComet eventComet, float movementSpeed, float rotationSpeed)
    {
        this.eventComet = eventComet;
        this.movementSpeed = movementSpeed;
        this.rotationSpeed = rotationSpeed;
    }
    private void DefineRotationDirection()
    {
        int aux = Random.Range(0, 2);
        int sign = 0;
        if (aux == 0) { sign = -1; }
        else if (aux == 1) { sign = +1; }
        else { Debug.LogError("HOW?????"); }

        rotationDirection = new Vector3(0, sign, 0);
    }
    private void MoveAndRotate()
    {
        transform.position += Vector3.forward * movementSpeed * Time.fixedDeltaTime;
        transform.Rotate(rotationDirection * rotationSpeed * Time.fixedDeltaTime);
    }
    private void Start()
    {
        DefineRotationDirection();
    }
    private void FixedUpdate()
    {
        MoveAndRotate();
    }
    public override void BeDestroyed()
    {
        EventHandler.Instance.SetTimer(9);
        base.BeDestroyed();
    }
}