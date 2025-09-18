using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [Header("Waypoints in de juiste volgorde")]
    public List<Transform> waypoints = new List<Transform>();

    [Header("Snelheid van de enemy")]
    public float moveSpeed = 2f;

    [Header("Enemy stats")]
    public int maxHealth = 5;   // instelbaar in de Inspector
    private int currentHealth;

    private int currentIndex = 0;

    private WaypointHolder waypointHolder;  

    void Start()
    {

        waypointHolder = GameObject.FindGameObjectWithTag("WayPointHolder").GetComponent<WaypointHolder>();

        waypoints = waypointHolder.GetWayPoints();
        if (waypoints.Count == 0)
        {
            Debug.LogError("Geen waypoints ingesteld voor " + gameObject.name);
            return;
        }

        // Startpositie = eerste waypoint
        transform.position = waypoints[0].position;

        // Zorg dat enemy zichtbaar is
        if (GetComponent<SpriteRenderer>() == null)
        {
            SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
            sr.color = Color.red; // rode kleur als placeholder
        }

      

        // Stel health in
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentIndex >= waypoints.Count) return;

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
                Destroy(gameObject); // Of doe hier iets anders, bv. damage aan speler
            }
        }
    }

    // Deze functie kan door een tower worden aangeroepen
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
