using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// EnemySpawner must be on the same object as a PlayerController, so the enemies will spawn around the player
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float SpawnDistance = 15f;
    [SerializeField] private float SpawnInterval = 1f;

    [SerializeField] private GameObject EnemyPrefab;

    public void Start()
    {
        Debug.Log("SPWANER START");
        StartCoroutine("Spawn");
    }

    public void OnDestroy()
    {
        StopCoroutine(nameof(Spawn));
    }

    private IEnumerable Spawn()
    {
        Debug.Log("START SPAWN");
        if (!EnemyPrefab)
        {
            yield break;
        }

        Debug.Log("SUCCESS");

        while (true)
        {
            float randomAngle = Random.Range(0, 360);
            Debug.Log(randomAngle);
            Vector3 spawnDirection = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle));
            Vector3 spawnPosition = transform.position + spawnDirection * SpawnDistance;

            Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity);
            
            yield return new WaitForSeconds(SpawnInterval);
        }
    }
}
