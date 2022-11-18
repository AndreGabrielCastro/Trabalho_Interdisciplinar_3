using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAsteroidRing : MonoBehaviour
{
    [Header("Must be setted")]
    [Tooltip("Must be EventObjectAsteroid")] public GameObject asteroidPrefab;
    public int asteroidAmount = 10;
    public int spawnTime = 3;
    private int remainingAsteroids;
    private float timer;
    private Transform spawnPointTransform;
    private void Start()
    { 
        spawnPointTransform = this.transform.Find("Spawn").transform;
        remainingAsteroids = asteroidAmount;
    }
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= spawnTime)
        {
            Vector3 spawnPosition = spawnPointTransform.position;
            Vector3 spawnRange = Vector3.right * Random.Range(-10, 10);
            spawnPosition += spawnRange;
            Instantiate(asteroidPrefab, spawnPosition, spawnPointTransform.rotation);
            timer = 0;
            remainingAsteroids -= 1;
            if (remainingAsteroids <= 0) { Destroy(this.gameObject); }
        }
    }
}
