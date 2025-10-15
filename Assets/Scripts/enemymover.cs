using UnityEngine;
using System.Collections.Generic;

public class EnemyMover : MonoBehaviour
{
    [Header("Snelheid van de enemy")]
    public float moveSpeed = 2f;

    [Header("Enemy stats")]
    public int maxHealth = 5;   // instelbaar in de Inspector
    private int currentHealth;

    private List<Transform> waypoints = new List<Transform>();
    private int currentIndex = 0;

    void Start()
    {
        // 🔹 Probeer automatisch het WaypointHolder object te vinden
        WaypointHolder holder = FindObjectOfType<WaypointHolder>();
        if (holder == null)
        {
            Debug.LogError("Geen WaypointHolder gevonden in de scene!");
            return;
        }

        // 🔹 Haal de waypoints op uit de holder
        waypoints = holder.GetWayPoints();
        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError("Geen waypoints gevonden in WaypointHolder!");
            return;
        }

        // Startpositie = eerste waypoint
        transform.position = waypoints[0].position;

        // Zorg dat enemy zichtbaar is
        if (GetComponent<SpriteRenderer>() == null)
        {
            SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
            sr.color = Color.red; // placeholder kleur
        }

        // Stel health in
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (waypoints == null || waypoints.Count == 0 || currentIndex >= waypoints.Count)
            return;

        // Huidig doel
        Transform target = waypoints[currentIndex];

        // Beweeg richting het waypoint
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        // Check of enemy dichtbij genoeg is
        if (Vector2.Distance(transform.position, target.position) < 0.05f)
        {
            currentIndex++;

            // Enemy is klaar met route
            if (currentIndex >= waypoints.Count)
            {
                Debug.Log(gameObject.name + " heeft het einde bereikt!");
                Destroy(gameObject);
            }
        }
    }

    // Damage-functie
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
}
