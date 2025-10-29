using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI moneyTxt;

    [Header("Speler geld instellingen")]
    [SerializeField] private int startingMoney = 100;
    private int money;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        money = startingMoney;
        UpdateUI();
    }

    public bool CanSpend(int amount)
    {
        return money >= amount;
    }

    public void SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateUI();
        }
        else
        {
            Debug.Log("❌ Niet genoeg geld!");
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (moneyTxt != null)
            moneyTxt.text = "$: " + money.ToString();
    }
}
