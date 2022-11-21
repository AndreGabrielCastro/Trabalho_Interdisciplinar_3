using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAsteroidRing : MonoBehaviour
{
    [Header("Must be setted")]
    [Tooltip("Must be EventObjectAsteroid")] public GameObject asteroidPrefab;
    public int asteroidAmount = 50;
    public float spawnTimeMax = 2;
    public float spawnTimeMin = 1;
    private float timer;
    private float spawnTime = 0;
    private int remainingAsteroids;
    private Transform spawnPointTransform;
    private void Start()
    { 
        spawnPointTransform = this.transform.Find("Spawn").transform;
        remainingAsteroids = asteroidAmount;

        float valorAleatorio = Random.Range(-30, 30);
        Debug.Log("Valor aleatorio: " + valorAleatorio);
        spawnPointTransform.position += valorAleatorio * Vector3.right;
        spawnPointTransform.eulerAngles += 2 * valorAleatorio * Vector3.up;
    }
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= spawnTime)
        {
            Vector3 spawnPosition = spawnPointTransform.position;
            Vector3 spawnRange = Vector3.right * Random.Range(-20, 20);
            spawnPosition += spawnRange;
            Instantiate(asteroidPrefab, spawnPosition, spawnPointTransform.rotation);
            timer = 0;
            spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
            remainingAsteroids -= 1;
            if (remainingAsteroids <= 0) { Destroy(this.gameObject); }
        }
    }
}
