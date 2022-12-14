using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityCannon : MonoBehaviour
{
    [Header("Must be setted")]
    public Transform spawnPointProjectile;
    public GameObject projectilePrefab;
    public float reloadTime;
    private float timer = 0;
    private void Update()
    {
        if (Player.Instance.isEventRunning == false) { return; }
        if (timer < reloadTime) { return; }
        if (Input.GetMouseButton(0)) { timer = 0; Shoot(); }
    }
    private void FixedUpdate()
    {
        if (Player.Instance.isEventRunning == false) { return; }

        Vector3 mousePosition = MouseSystem.Instance.GetWorldPosition();
        mousePosition = new Vector3(mousePosition.x, this.transform.position.y, mousePosition.z);
        this.transform.LookAt(mousePosition);

        if (timer < reloadTime) { timer += Time.fixedDeltaTime; }
    }
    private void Shoot()
    { Instantiate(projectilePrefab, spawnPointProjectile.transform.position, spawnPointProjectile.transform.rotation); }
}
