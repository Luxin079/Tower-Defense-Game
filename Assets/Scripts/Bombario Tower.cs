using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Stats")]
    public float range = 5f;
    public float shootCooldown = 1f;
    public int damage = 1;

    [Header("Enemy Targeting")]
    public string enemyTag = "Enemy";

    [Header("Shooting")]
    public Transform firePoint;
    public GameObject bulletPrefab;

    private EnemyMover currentTarget;
    public EnemyMover CurrentTarget => currentTarget; // read-only getter
    private float shootTimer = 0f;

    void Update()
    {
        if (currentTarget == null)
        {
            FindTarget();
        }
        else
        {
            if (currentTarget == null) return;

            float distance = Vector2.Distance(transform.position, currentTarget.transform.position);
            if (distance > range)
            {
                currentTarget = null;
                return;
            }

            // Tower draait altijd naar enemy
            Vector2 dir = currentTarget.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Schiet cooldown
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                Shoot();
                shootTimer = shootCooldown;
            }
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float closestDistance = Mathf.Infinity;
        EnemyMover closestEnemy = null;

        foreach (GameObject enemyObj in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemyObj.transform.position);
            if (dist <= range && dist < closestDistance)
            {
                closestDistance = dist;
                closestEnemy = enemyObj.GetComponent<EnemyMover>();
            }
        }

        if (closestEnemy != null)
        {
            currentTarget = closestEnemy;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Spawn bullet in firePoint-richting
            GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // damage doorgeven
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.damage = damage;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
