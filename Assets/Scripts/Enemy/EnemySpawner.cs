using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 3;       
    public float spawnRadius = 10f;  

    void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 randomPos = RandomPointOnNavMesh(transform.position, spawnRadius);
            Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        }
    }

    private static Vector3 RandomPointOnNavMesh(Vector3 origin, float radius)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPoint = origin + Random.insideUnitSphere * radius;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, radius, NavMesh.AllAreas))
                return hit.position;
        }
        return origin;
    }
}
