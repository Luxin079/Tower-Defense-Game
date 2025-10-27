using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MoneyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI moneyTxt;

    private int money = 100;
    void Start()
    {
        UpdateUI(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddMoney(int a)
    {
        money += a;
        UpdateUI(); 
    }
    private void UpdateUI()
    {
        moneyTxt.text = "$: " + money.ToString();   
    }
}
