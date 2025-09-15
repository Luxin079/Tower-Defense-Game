using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public float lifeTime = 3f;
    public int damage = 1;

    void Start()
    {
        // bullet verdwijnt sowieso na X seconden
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // beweeg altijd rechtdoor in de local "right" richting van het firePoint
        transform.position += transform.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            EnemyMover enemy = collision.GetComponent<EnemyMover>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject); // bullet weg na hit
        }
    }
}
