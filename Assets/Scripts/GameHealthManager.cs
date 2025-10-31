using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameHealthManager : MonoBehaviour
{
    public static GameHealthManager Instance;

    [Header("Health Settings")]
    public int maxLives = 10;
    private int currentLives;

    [Header("UI")]
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;

    [HideInInspector]
    public bool IsGameOver { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void EnemyReachedCheckpoint()
    {
        if (IsGameOver) return;

        currentLives--;
        UpdateLivesUI();

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = $"Lives: {currentLives}/{maxLives}";
    }

    private void GameOver()
    {
        IsGameOver = true;

        Debug.Log("💀 Game Over!");
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Destroy alle actieve enemies
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        // Reset na 3 seconden
        Invoke(nameof(RestartGame), 3f);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
