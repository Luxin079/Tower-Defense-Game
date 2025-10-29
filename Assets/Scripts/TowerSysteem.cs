using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [Header("Tower Info")]
    public GameObject towerPrefab;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnButtonClick);
        else
            Debug.LogError("❌ Geen Button component op " + gameObject.name);
    }

    private void OnButtonClick()
    {
        if (towerPrefab == null)
        {
            Debug.LogError("❌ Geen towerPrefab ingesteld op " + gameObject.name);
            return;
        }

        if (BuildManager.Instance != null)
        {
            BuildManager.Instance.SetSelectedTower(towerPrefab);
            Debug.Log("Tower geselecteerd voor plaatsing.");
        }
        else
            Debug.LogError("❌ BuildManager.Instance is null!");
    }
}
