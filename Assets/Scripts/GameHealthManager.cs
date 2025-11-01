using UnityEngine;
using TMPro;

public class GameHealthManager : MonoBehaviour
{
    public static GameHealthManager Instance;

    [Header("Instellingen")]
    [SerializeField] private int maxLives = 10;
    private int currentLives;
    private bool isGameOver = false;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI livesText; // Sleep hier je TMP-text object in
    [SerializeField] private GameObject gameOverPanel;   // optioneel (kan leeg blijven)

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = $"Lives: {currentLives}";
        else
            Debug.LogWarning("⚠️ LivesText niet ingesteld in GameHealthManager!");
    }

    public void LoseLife()
    {
        if (isGameOver)
            return;

        currentLives--;
        UpdateLivesUI();

        if (currentLives <= 0)
            GameOver();
    }

    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("💀 GAME OVER!");

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public bool IsGameOver => isGameOver;
    public int GetLives() => currentLives;
}
