using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Must be setted")]
    [SerializeField] private float speed = 1; public void AlterateSpeed(float value) { speed += value; }

    void Update()
    {
        if (Player.Instance.isGameOver == true) { return; }
        if (Player.Instance.isTravelling == false) { return; }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        this.transform.position += direction * speed * Time.deltaTime;

        return;

        // The spaceship is so fast that a single degree could make him get out of the route. The code below makes no sense to the LORE

        //Vector3 rotation = new Vector3(0, horizontal, 0);
        //this.transform.eulerAngles += rotation * Time.deltaTime;

        //if (rotation == Vector3.zero)
        //{ this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.identity, 0.5f * Time.deltaTime).normalized; }
    }
}