using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnRate = 5f;
    public float spawnRadius = 5f;
    public int maxEnemiesAlive = 10;
    public int totalSpawnLimit = 100;
    public EnemyCountDisplay enemyCountDisplay;

    private int currentAliveCount = 0;
    private int totalSpawned = 0;
    private int totalKilled = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentAliveCount < maxEnemiesAlive && totalSpawned < totalSpawnLimit)
            {
                Vector3 spawnPos = GetRandomSpawnPosition2D();

                if (spawnPos != Vector3.zero)
                {
                    GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

                    EnemyHealth eh = newEnemy.GetComponent<EnemyHealth>();
                    if (eh != null)
                        eh.spawner = this;

                    currentAliveCount++;
                    totalSpawned++;

                    if (enemyCountDisplay != null)
                        enemyCountDisplay.UpdateCount(totalKilled, totalSpawnLimit);
                }
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }

    Vector3 GetRandomSpawnPosition2D()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector2 randomPos = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;
            if (Vector2.Distance(randomPos, player.position) < 5f) continue;
            return new Vector3(randomPos.x, randomPos.y, 0);
        }
        return Vector3.zero;
    }

    public void OnEnemyKilled()
    {
        currentAliveCount = Mathf.Max(0, currentAliveCount - 1);
        totalKilled++;

        if (enemyCountDisplay != null)
            enemyCountDisplay.UpdateCount(totalKilled, totalSpawnLimit);

        if (totalKilled >= totalSpawnLimit && currentAliveCount == 0)
        {
            StopAllCoroutines();

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                Destroy(enemy);

            foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
                Destroy(bullet);
        }
    }
}