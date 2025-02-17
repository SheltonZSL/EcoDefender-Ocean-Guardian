using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;       // Movement speed of the projectile
    public float lifetime = 3f;    // Maximum lifetime of the projectile

    [Header("Damage")]
    public float damage = 3f;      // Damage dealt to the enemy

    private Transform target;      // The enemy target
    private Rigidbody2D rb;

    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("[EnergyBall] Missing Rigidbody2D on " + gameObject.name);
        }
        // Destroy the projectile after a set lifetime to prevent infinite existence
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        // If the target no longer exists (destroyed or out of range), destroy the projectile
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Calculate the direction towards the target and move using Rigidbody2D
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the projectile collides with an object tagged as 'Enemy', deal damage and destroy the projectile
        if (other.CompareTag("Enemy"))
        {
            Enemy enemyScript = other.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
