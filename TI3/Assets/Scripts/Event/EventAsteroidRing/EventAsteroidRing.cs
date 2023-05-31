using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAsteroidRing : Event
{
    [Header("Must be setted")]
    [SerializeField] private GameObject vfxAsteroidHitted;
    [SerializeField] private GameObject vfxAsteroidDestroyed;
    [Tooltip("Must be EventObjectAsteroid")] public GameObject asteroidPrefab;
    [SerializeField] private int asteroidAmount = 100;
    [SerializeField] private float spawnTimeMax = 0.5f;
    [SerializeField] private float spawnTimeMin = 0.2f;
    private float timer;
    private float spawnTime = 0;
    private int remainingAsteroids;
    private Transform spawnPointTransform;
    public GameObject GetVFXAsteroidHitted() { return vfxAsteroidHitted; }
    public GameObject GetVFXAsteroidDestroyed() { return vfxAsteroidDestroyed; }
    private void Start()
    {
        PlaySoundTrack();

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
            Vector3 spawnRange = Vector3.right * Random.Range(-40, 40);
            spawnPosition += spawnRange;
            Instantiate(asteroidPrefab, spawnPosition, spawnPointTransform.rotation);
            timer = 0;
            spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
            remainingAsteroids -= 1;
            if (remainingAsteroids <= 0)
            {
                EventHandler.Instance.SetTimer(10);
                Destroy(this.gameObject);
            }
        }
    }
}