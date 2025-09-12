using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [Header("Sleep hier je checkpoints/waypoints in de juiste volgorde")]
    public Transform[] waypoints;

    [Header("Snelheid van de enemy")]
    public float moveSpeed = 2f;

    private int currentIndex = 0;

    void Start()
    {
        if (waypoints.Length == 0)
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
    }

    void Update()
    {
        if (currentIndex >= waypoints.Length) return;

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
            if (currentIndex >= waypoints.Length)
            {
                Debug.Log(gameObject.name + " heeft het einde bereikt!");
                Destroy(gameObject); // Of doe hier iets anders, bv. damage aan speler
            }
        }
    }
}
