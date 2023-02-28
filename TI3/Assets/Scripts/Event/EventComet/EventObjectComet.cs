using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObjectComet : EventObject
{
    public float movementSpeed = 0.1f;
    public float rotationSpeed = 10;
    private Vector3 rotationDirection;
    private void Start()
    {
        int aux = Random.Range(0, 2);
        int sign = 0;
        if (aux == 0) { sign = -1; }
        else if (aux == 1) { sign = +1; }
        else { Debug.LogError("HOW?????"); }

        rotationDirection = new Vector3(0, sign, 0);
    }
    private void FixedUpdate()
    {
        transform.position += Vector3.forward * movementSpeed * Time.fixedDeltaTime;
        transform.Rotate(rotationDirection * rotationSpeed * Time.fixedDeltaTime);
    }
}
