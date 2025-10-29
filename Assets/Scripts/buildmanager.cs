using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    [Header("Huidig geselecteerde tower")]
    public GameObject selectedTowerPrefab;

    [Header("Preview settings")]
    private GameObject previewTower;
    private Renderer[] previewRenderers;
    private MonoBehaviour[] previewBehaviours;

    private Camera cam;

    [Header("Tower Placement")]
    public float towerHeightOffset = 0.0f;
    public string allowedTag = "BuildArea"; // Alleen hier mag gebouwd worden

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        cam = Camera.main;
        if (cam == null)
            Debug.LogError("❌ Geen main camera gevonden in de scene!");
    }

    void Update()
    {
        if (selectedTowerPrefab == null || cam == null) return;

        // Maak preview als die nog niet bestaat
        if (previewTower == null)
        {
            previewTower = Instantiate(selectedTowerPrefab);
            previewTower.layer = LayerMask.NameToLayer("Ignore Raycast");

            // Disable alle scripts
            previewBehaviours = previewTower.GetComponentsInChildren<MonoBehaviour>();
            foreach (var script in previewBehaviours)
                script.enabled = false;

            // Maak transparant
            previewRenderers = previewTower.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in previewRenderers)
            {
                foreach (Material m in r.materials)
                    m.color = new Color(m.color.r, m.color.g, m.color.b, 0.5f);
            }
        }

        // Volg muis
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 placePos = new Vector3(Mathf.Round(hit.point.x), hit.point.y + towerHeightOffset, Mathf.Round(hit.point.z));
            previewTower.transform.position = placePos;

            TowerStatus towerStatus = selectedTowerPrefab.GetComponent<TowerStatus>();
            int towerCost = towerStatus != null ? towerStatus.towerCost : 0;

            bool canAfford = MoneyManager.Instance.CanSpend(towerCost);
            bool canBuildHere = hit.collider.CompareTag(allowedTag);

            // 🔴 Rood als het niet kan, 🟢 wit als het mag
            UpdatePreviewColor(canAfford && canBuildHere);

            // Klik = plaatsen
            if (Input.GetMouseButtonDown(0))
            {
                if (canBuildHere)
                    TryPlaceTower(placePos);
                else
                    Debug.Log("❌ Je kunt hier geen tower plaatsen!");
            }
        }
    }

    private void TryPlaceTower(Vector3 position)
    {
        TowerStatus towerStatus = selectedTowerPrefab.GetComponent<TowerStatus>();
        if (towerStatus == null)
        {
            Debug.LogError("❌ TowerStatus component ontbreekt op prefab!");
            return;
        }

        int towerCost = towerStatus.towerCost;

        if (MoneyManager.Instance != null && MoneyManager.Instance.CanSpend(towerCost))
        {
            Instantiate(selectedTowerPrefab, position, Quaternion.identity);
            MoneyManager.Instance.SpendMoney(towerCost);
            Debug.Log($"✅ Tower geplaatst voor {towerCost} coins!");
        }
        else
        {
            Debug.Log("❌ Niet genoeg geld om deze tower te plaatsen!");
        }

        // Clear preview
        if (previewTower != null)
            Destroy(previewTower);

        selectedTowerPrefab = null;
    }

    private void UpdatePreviewColor(bool canPlace)
    {
        if (previewRenderers == null) return;

        Color c = canPlace ? new Color(1, 1, 1, 0.5f) : new Color(1, 0, 0, 0.5f);
        foreach (Renderer r in previewRenderers)
        {
            foreach (Material m in r.materials)
                m.color = c;
        }
    }

    public void SetSelectedTower(GameObject towerPrefab)
    {
        if (previewTower != null)
            Destroy(previewTower);

        selectedTowerPrefab = towerPrefab;
    }
}
