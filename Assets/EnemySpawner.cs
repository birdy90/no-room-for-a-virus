using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// EnemySpawner must be on the same object as a PlayerController, so the enemies will spawn around the player
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float SpawnDistance = 30;
    [SerializeField] private float SpawnInterval = 1f;

    [SerializeField] private Enemy EnemyPrefab;

    public void Start()
    {
        InvokeRepeating(nameof(Spawn), 0, SpawnInterval);
    }

    public void OnDestroy()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        if (!EnemyPrefab)
        {
            return;
        }

        float randomAngle = Random.Range(0, 360);
        Vector3 spawnDirection = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle));
        Vector3 spawnPosition = transform.position + spawnDirection * SpawnDistance;

        Enemy enemy = Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity);
        enemy.SetPlayerTransform(transform);
    }
}
