using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyGroup
    {
        public GameObject enemyPrefab;  // welk enemy prefab
        public int count;               // hoeveel enemies van dit type
        public float spawnRate = 1f;    // hoe snel deze groep spawnt
    }

    [System.Serializable]
    public class Wave
    {
        public string waveName;         // naam voor overzicht
        public EnemyGroup[] enemyGroups; // groepen enemies in deze wave
    }

    [Header("Waves")]
    public Wave[] waves;               // lijst met waves
    private int currentWaveIndex = 0;

    [Header("Spawn Settings")]
    public Transform spawnPoint;       // waar enemies verschijnen
    public float timeBetweenWaves = 5f;

    private float waveCountdown;
    private bool isSpawning = false;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (!isSpawning)
        {
            if (waveCountdown <= 0f)
            {
                if (currentWaveIndex < waves.Length)
                {
                    StartCoroutine(SpawnWave(waves[currentWaveIndex]));
                    currentWaveIndex++;
                }
                else
                {
                    Debug.Log("Alle waves zijn klaar!");
                }
                waveCountdown = timeBetweenWaves;
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Start wave: " + wave.waveName);
        isSpawning = true;

        // Voor elke groep enemies in de wave
        foreach (EnemyGroup group in wave.enemyGroups)
        {
            for (int i = 0; i < group.count; i++)
            {
                SpawnEnemy(group.enemyPrefab);
                yield return new WaitForSeconds(1f / group.spawnRate);
            }
        }

        isSpawning = false;
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
