using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class SniperinoBullet : MonoBehaviour
{
    [Header("Bullet instellingen")]
    public float speed = 15f;
    public int damage = 2;
    public float lifeTime = 2f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Zet Rigidbody2D juist op zodat hij niet blijft plakken
        rb.gravityScale = 0f;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = true;

        // Zorg dat de Collider2D een trigger is
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void Start()
    {
        // Bullet verdwijnt vanzelf na een bepaalde tijd
        Destroy(gameObject, lifeTime);
    }

    // Richting waarin de kogel vliegt
    public void Fire(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            EnemyMover enemy = collision.GetComponent<EnemyMover>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Geen Destroy — bullet vliegt door!
        }
    }
}
