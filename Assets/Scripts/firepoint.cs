using UnityEngine;

public class FirePointAim : MonoBehaviour
{
    [Header("Reference")]
    public Tower tower; // Verwijzing naar je Tower script

    void Update()
    {
        if (tower.CurrentTarget != null)
        {
            Vector2 dir = tower.CurrentTarget.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90f);
        }
    }
   }

