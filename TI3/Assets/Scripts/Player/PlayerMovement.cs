using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Must be setted")]
    public float speed = 1;
    [Header("Setted during playtime")]
    public bool isEventRunning;
    [HideInInspector] public bool isGameOver;
    void Update()
    {
        if (isGameOver == true) { return; }
        if (isEventRunning == false) { return; }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        this.transform.position += direction * speed * Time.deltaTime;

        Vector3 rotation = new Vector3(0, horizontal, 0);
        this.transform.eulerAngles += rotation * Time.deltaTime;

        if (rotation == Vector3.zero)
        { this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.identity, 0.5f * Time.deltaTime).normalized; }
    }
}