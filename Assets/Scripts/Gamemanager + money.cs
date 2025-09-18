using UnityEngine;
using TMPro; // <- belangrijk!

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Speler geld")]
    public int startingMoney = 100;
    public int money;

    [Header("UI")]
    public TextMeshProUGUI moneyText; // <- TextMeshPro variant

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        money = startingMoney;
        UpdateUI();
    }

    public bool SpendMoney(int amount)
    {
        if (money < amount)
        {
            Debug.Log("Niet genoeg geld!");
            return false;
        }

        money -= amount;
        UpdateUI();
        return true;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = "Geld: " + money;
    }
}
