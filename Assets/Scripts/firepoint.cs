using UnityEngine;

public class FirePointAim : MonoBehaviour
{
    [Header("Reference")]
    public BombarioTower tower;

    void Update()
    {
        if (tower.CurrentTarget != null)
        {
            Vector2 dir = tower.CurrentTarget.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // 🔹 Hier pas je offset aan als sprite niet goed draait
            transform.rotation = Quaternion.Euler(0, 180, angle);
        }
    }
}
