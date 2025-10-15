using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [Header("Tower Settings")]
    public GameObject towerPrefab;
    public float checkRadius = 0.5f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 placePos = hit.point;

                // Snap tower to grid (optioneel)
                placePos = new Vector3(Mathf.Round(placePos.x), placePos.y, Mathf.Round(placePos.z));

                // Check if tower can be placed here
                Collider[] colliders = Physics.OverlapSphere(placePos, checkRadius);
                bool canPlace = true;

                foreach (Collider col in colliders)
                {
                    // Als er een tower in de buurt is, niet plaatsen
                    if (col.gameObject.layer == LayerMask.NameToLayer("Tower") ||
                        col.gameObject.layer == LayerMask.NameToLayer("Road"))
                    {
                        canPlace = false;
                        break;
                    }
                }

                if (canPlace)
                {
                    GameObject tower = Instantiate(towerPrefab, placePos, Quaternion.identity);
                    tower.layer = LayerMask.NameToLayer("Tower");
                    Debug.Log("✅ Tower placed!");
                }
                else
                {
                    Debug.Log("❌ Can't place tower here (too close to another tower or road)");
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(new Vector3(Mathf.Round(hit.point.x), hit.point.y, Mathf.Round(hit.point.z)), checkRadius);
            }
        }
    }
}
