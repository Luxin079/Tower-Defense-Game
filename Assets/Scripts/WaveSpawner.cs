using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyGroup
    {
        public GameObject enemyPrefab;
        public int count;
        public float spawnRate = 1f;
    }

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public EnemyGroup[] enemyGroups;
    }

    [Header("Waves")]
    public Wave[] waves;
    private int currentWaveIndex = 0;

    [Header("Spawn Settings")]
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;

    private float waveCountdown;
    private bool isSpawning = false;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private int totalWaveCount = 20; // 🔹 Aantal waves voor UI weergave

    private int baseReward = 100;
    private float rewardMultiplier = 1f;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
        UpdateWaveText();
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
                    GiveWaveReward();
                    UpdateWaveText();
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

    private void GiveWaveReward()
    {
        int reward = Mathf.RoundToInt(baseReward * rewardMultiplier);
        MoneyManager.Instance.AddMoney(reward);
        Debug.Log($"💰 Wave reward: {reward}");

        // Elke 3 waves → +15% meer geld
        if (currentWaveIndex % 3 == 0)
        {
            rewardMultiplier += 0.15f;
            Debug.Log($"⬆️ Geld multiplier verhoogd naar: {rewardMultiplier * 100 - 100:F0}% extra");
        }
    }

    private void UpdateWaveText()
    {
        if (waveText != null)
        {
            int displayWave = Mathf.Clamp(currentWaveIndex + 1, 1, totalWaveCount);
            waveText.text = $"Wave: {displayWave}/{totalWaveCount}";
        }
    }
}
