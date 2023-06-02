using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAsteroidRing : Event
{
    [Header("Must be setted")]
    [SerializeField] private GameObject vfxAsteroidHitted; public GameObject GetVFXAsteroidHitted() { return vfxAsteroidHitted; }
    [SerializeField] private GameObject vfxAsteroidDestroyed; public GameObject GetVFXAsteroidDestroyed() { return vfxAsteroidDestroyed; }
    [SerializeField] private AudioClip sfxAsteroidHitted; public AudioClip GetSFXAsteroidHitted() { return sfxAsteroidHitted; }
    [SerializeField] private AudioClip sfxAsteroidDestroyed; public AudioClip GetSFXAsteroidDestroyed() { return sfxAsteroidDestroyed; }

    [Tooltip("Must be EventObjectAsteroid")] public EventObjectAsteroid asteroidPrefab;
    [SerializeField] private int asteroidAmount = 100;
    [SerializeField] private float spawnTimeMax = 0.5f;
    [SerializeField] private float spawnTimeMin = 0.2f;
    private float timer;
    private float spawnTime = 0;
    private int remainingAsteroids;
    private Transform spawnPointTransform;
    
    
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
            EventObjectAsteroid asteroid = Instantiate(asteroidPrefab, spawnPosition, spawnPointTransform.rotation);
            asteroid.SetEventAsteroidRing(this);
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