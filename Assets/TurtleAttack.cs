using UnityEngine;

public class TurtleAttack : MonoBehaviour
{
    [Header("Prefab & FirePoint")]
    public GameObject energyBallPrefab;   // Prefab of the energy ball projectile
    public Transform firePoint;           // The point from where the projectile is fired (usually a child transform)

    [Header("Attack Settings")]
    public float attackCooldown = 1f;     // Time in seconds between attacks
    public float detectionRadius = 5f;    // Radius for detecting enemies
    public LayerMask enemyLayer;          // LayerMask that corresponds to enemies (set in the Inspector)

    private float lastAttackTime = 0f;

    void Update()
    {
        GameObject enemy = DetectEnemy();

        // If an enemy is found and the cooldown is over, fire an energy ball
        if (enemy != null && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack(enemy.transform);
            lastAttackTime = Time.time;
        }
    }

    GameObject DetectEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        if (colliders.Length > 0)
        {
            return colliders[0].gameObject;
        }
        return null;
    }

    void Attack(Transform enemy)
    {
        if (energyBallPrefab == null || firePoint == null)
        {
            Debug.LogError("[TurtleAttack] EnergyBallPrefab or FirePoint is missing in the Inspector!");
            return;
        }

        // Instantiate the energy ball
        GameObject energyBall = Instantiate(energyBallPrefab, firePoint.position, Quaternion.identity);
        EnergyBall ballScript = energyBall.GetComponent<EnergyBall>();
        if (ballScript != null)
        {
            ballScript.SetTarget(enemy);
        }
        else
        {
            Debug.LogError("[TurtleAttack] The generated energy ball is missing the EnergyBall script!");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
