﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityCannon : MonoBehaviour
{
    private AudioSource audioSource;
    [Header("Must be setted")]
    [SerializeField] private Transform spawnPointProjectile;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private AudioClip[] projectileSfxArray;
    [SerializeField] private int shootEnergyCost;
    [SerializeField] private float reloadTime;
    private float timer = 0;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Player.Instance.isTravelling == false) { return; }
        if (Player.Instance.isGameOver == true) { return; }
        if (timer < reloadTime) { return; }
        if (Input.GetMouseButton(1)) { timer = 0; Shoot(); }
    }
    private void FixedUpdate()
    {
        if (Player.Instance.isTravelling == false) { return; }

        Vector3 mousePosition = MouseSystem.Instance.GetWorldPosition();
        mousePosition = new Vector3(mousePosition.x, this.transform.position.y, mousePosition.z);
        this.transform.LookAt(mousePosition);

        if (timer < reloadTime) { timer += Time.fixedDeltaTime; }
    }
    private void Shoot()
    {
        if (Player.Instance.playerEnergy.GetCurrentEnergy() < shootEnergyCost) { return; }
        Instantiate(projectilePrefab, spawnPointProjectile.transform.position, spawnPointProjectile.transform.rotation);
        audioSource.PlayOneShot(projectileSfxArray[Random.Range(0, 4)]);
        Player.Instance.playerEnergy.LoseEnergy(shootEnergyCost);
    }
}
