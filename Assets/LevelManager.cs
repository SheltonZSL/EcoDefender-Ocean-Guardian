using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("Path Points")]
    public Transform[] path;         // The path that enemies will follow (from index 0 -> 1 -> 2, etc.)

    [Header("Enemy Spawning")]
    public GameObject enemyPrefab;   // The enemy prefab to spawn
    public float spawnInterval = 2f; // How many seconds to wait between spawns
    public int spawnCount = 5;       // How many enemies to spawn in total

    private void Awake()
    {
        // Set this as a global singleton so it can be accessed by other scripts
        main = this;
    }

    private void Start()
    {
        // Start a coroutine to spawn enemies repeatedly at the beginning
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // Instantiate the enemy at path[0]'s position as the starting point
            Instantiate(enemyPrefab, path[0].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
