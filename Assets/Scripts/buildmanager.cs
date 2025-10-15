using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    [Header("Welke tower is geselecteerd om te bouwen")]
    public GameObject selectedTowerPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetSelectedTower()
    {
        return selectedTowerPrefab;
    }

    public void SetSelectedTower(GameObject tower)
    {
        selectedTowerPrefab = tower;
    }
}
