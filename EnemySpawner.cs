using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject portalPrefab;
    public Transform[] portalSpawnPoints;

    public int minEnemiesPerPortal = 1;
    public int maxEnemiesPerPortal = 2;
    public float spawnDelayBetweenEnemies = 1.5f;
    public float portalDuration = 10f;
    public float initialSpawnDelay = 5f;   // Delay before first wave
    public float timeBetweenWaves = 28f;   // Delay between waves
    public int totalRounds = 2;            // Total number of waves

    public AudioSource AudioSource;
    public AudioClip AudioClip;

    private int currentRound = 0;

    void Start()
    {
        Debug.Log("Enemy Spawner Initialized.");
       
    }

  public IEnumerator SpawnMultipleWaves()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        while (currentRound < totalRounds)
        {
            Debug.Log($"Starting Wave {currentRound + 1}");
            StartWave();
            currentRound++;

            yield return new WaitForSeconds(timeBetweenWaves);
        }

        Debug.Log("All waves completed.");
    }

    public void StartWave()
    {
        if (AudioSource && AudioClip)
        {
            AudioSource.PlayOneShot(AudioClip);
        }

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < portalSpawnPoints.Length; i++)
        {
            Transform portalPoint = portalSpawnPoints[i];

            if (portalPoint == null)
            {
                Debug.LogWarning($"Portal spawn point {i} is null!");
                continue;
            }

            GameObject portal = Instantiate(portalPrefab, portalPoint.position, portalPoint.rotation);
            portal.name = "Portal_" + i;

            StartCoroutine(SpawnEnemiesFromPortal(portalPoint, portal, i));
        }

        yield return null;
    }

    IEnumerator SpawnEnemiesFromPortal(Transform spawnPoint, GameObject portal, int portalIndex)
    {
        int enemyCount = Random.Range(minEnemiesPerPortal, maxEnemiesPerPortal + 1);

        Debug.Log($"[Wave {currentRound + 1}] Portal {portalIndex + 1} spawning {enemyCount} enemies.");

        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(spawnDelayBetweenEnemies);
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemy.name = $"Enemy_Portal{portalIndex + 1}_#{i + 1}";
        }

        Destroy(portal, portalDuration);
    }
}
