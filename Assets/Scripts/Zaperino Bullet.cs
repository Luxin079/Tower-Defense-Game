using UnityEngine;

public class ZaperinoBullet : MonoBehaviour
{
    public float speed = 12f;
    public float slowAmount = 0.5f;   // 50% langzamer
    public float slowDuration = 2f;   // 2 seconden traag
    public int damage = 1;

    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Draait richting vijand
        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }



private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            EnemyMover enemy = collision.GetComponent<EnemyMover>();
            if (enemy != null)
            {
                // Hier gaan we de vijand vertragen
                enemy.ApplySlow(slowAmount, slowDuration);
                enemy.TakeDamage(damage);

            }

            Destroy(gameObject);
        }
    }
}
