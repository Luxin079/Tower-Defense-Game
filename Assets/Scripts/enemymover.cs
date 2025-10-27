using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMover : MonoBehaviour
{
    [Header("Snelheid van de enemy")]
    public float moveSpeed = 2f;

    [Header("Enemy stats")]
    public int maxHealth = 5;
    private int currentHealth;

    private List<Transform> waypoints = new List<Transform>();
    private int currentIndex = 0;

    // 🔹 Voor slow-effect
    private float currentSpeed;
    private Coroutine slowRoutine;

    void Start()
    {
        WaypointHolder holder = FindObjectOfType<WaypointHolder>();
        if (holder == null)
        {
            Debug.LogError("Geen WaypointHolder gevonden in de scene!");
            return;
        }

        waypoints = holder.GetWayPoints();
        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError("Geen waypoints gevonden in WaypointHolder!");
            return;
        }

        transform.position = waypoints[0].position;

        if (GetComponent<SpriteRenderer>() == null)
        {
            SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
            sr.color = Color.red;
        }

        currentHealth = maxHealth;
        currentSpeed = moveSpeed; // begin op normale snelheid
    }

    void Update()
    {
        if (waypoints == null || waypoints.Count == 0 || currentIndex >= waypoints.Count)
            return;

        Transform target = waypoints[currentIndex];

        // Gebruik currentSpeed i.p.v. moveSpeed zodat slow werkt
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            currentSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, target.position) < 0.05f)
        {
            currentIndex++;
            if (currentIndex >= waypoints.Count)
            {
                Debug.Log(gameObject.name + " heeft het einde bereikt!");
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " kreeg " + damage + " damage. Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " is vernietigd!");
        Destroy(gameObject);
    }

    // 🔹 Slow-functie (werkt met ZaperinoBullet)
    public void ApplySlow(float slowFactor, float duration)
    {
        // Stop vorige slow als er al een actief is
        if (slowRoutine != null)
            StopCoroutine(slowRoutine);

        slowRoutine = StartCoroutine(SlowEffect(slowFactor, duration));
    }

    private IEnumerator SlowEffect(float slowFactor, float duration)
    {
        currentSpeed = moveSpeed * slowFactor;
        yield return new WaitForSeconds(duration);
        currentSpeed = moveSpeed;
        slowRoutine = null;
    }
}
