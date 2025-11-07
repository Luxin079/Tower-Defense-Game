using UnityEngine;

public class SniperinoTower : MonoBehaviour
{
    [Header("Tower instellingen")]
    public float range = 10f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("References")]
    public string enemyTag = "Enemy";
    public Transform firePoint;
    public GameObject bulletPrefab;

    private Transform target;

    public Transform CurrentTarget => target;

    void Update()
    {
        // Zoek nieuwe target als nodig
        if (target == null || Vector2.Distance(transform.position, target.position) > range)
        {
            target = null;
            UpdateTarget();
        }

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
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        SniperinoBullet bullet = bulletGO.GetComponent<SniperinoBullet>();

        if (bullet != null && target != null)
        {
            Vector2 dir = (target.position - firePoint.position).normalized;
            bullet.Fire(dir);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
