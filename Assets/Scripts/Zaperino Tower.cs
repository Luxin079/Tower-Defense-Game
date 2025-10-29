using UnityEngine;

public class ZaperinoTower : MonoBehaviour
{
    [Header("Tower instellingen")]
    public int cost = 100;
    public float range = 6f;
    public float fireRate = 1.5f;
    private float fireCountdown = 0f;

    [Header("References")]
    public string enemyTag = "Enemy";
    public Transform firePoint;
    public GameObject bulletPrefab;

    private Transform target;

    public Transform CurrentTarget
    {
        get { return target; }
    }

    void Update()
    {
        // Als er geen target is of de huidige target buiten range is, zoek een nieuwe
        if (target == null || Vector2.Distance(transform.position, target.position) > range)
        {
            target = null;
            UpdateTarget();
        }

        // Als er nog steeds geen target is, doe niks
        if (target == null)
            return;

        // Draai naar de enemy
        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        // Schiet met interval
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        ZaperinoBullet bullet = bulletGO.GetComponent<ZaperinoBullet>();

        if (bullet != null && target != null)
        {
            bullet.SetTarget(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
