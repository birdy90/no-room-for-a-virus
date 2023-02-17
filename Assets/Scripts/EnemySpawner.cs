using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// EnemySpawner must be on the same object as a PlayerController, so the enemies will spawn around the player
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float SpawnDistance = 30;
    [SerializeField] private float SpawnInterval = 1f;
    [SerializeField] private float EnemyLevelChangeInterval = 60f;
    [SerializeField] private UnitStats StatsDiffPerLevel;
    [SerializeField] private float EliteEnemiesSpawnInterval = 60f;
    [SerializeField] private float EliteEnemyMultiplier = 2f;
    [SerializeField] private int RandomizingRange = 2;

    [SerializeField] private Enemy EnemyPrefab;

    [SerializeField] private List<Texture> AvailableIcons;
    [ColorUsage(true,true)]
    [SerializeField] private List<Color> AvailableColors;

    private PlayerController _playerController;
    private int _currentEnemyLevel = 1;
    /// <summary>
    /// Set value here so it will skip first N seconds
    /// </summary>
    private float _lastEliteEnemySpawnTime = 0f;
    private static readonly int Color1 = Shader.PropertyToID("_Color");
    private static readonly int Sprite1 = Shader.PropertyToID("_Sprite");

    public void Start()
    {
        _playerController = GetComponent<PlayerController>();
        InvokeRepeating(nameof(Spawn), 0, SpawnInterval);
        StartCoroutine(nameof(IncreaseEnemyLevel));
    }

    public void OnDestroy()
    {
        CancelInvoke(nameof(Spawn));
        StopAllCoroutines();
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
        enemy.SetPlayerController(_playerController);

        // get spawn params
        int enemyLevel = GetRandomLevel();
        Color color = GetColorForLevel(enemyLevel);
        Texture icon = GetIconForLevel(enemyLevel);
        UnitStats stats = GetStatsForLevel(enemyLevel);
        
        // set visuals
        MeshRenderer enemyRenderer = enemy.GetComponentInChildren<MeshRenderer>();
        enemyRenderer.material.SetColor(Color1, color);
        enemyRenderer.material.SetTexture(Sprite1, icon);

        // try convert to elite enemy
        float timePartPassed = (Time.time - _lastEliteEnemySpawnTime) / EliteEnemiesSpawnInterval;
        if (Math.Pow(timePartPassed, 3) > Random.value)
        {
            _lastEliteEnemySpawnTime = Time.time;
            enemy.transform.localScale = new Vector3(EliteEnemyMultiplier, EliteEnemyMultiplier, EliteEnemyMultiplier);
            stats *= EliteEnemyMultiplier;
            stats.Speed = (_playerController.Stats.Speed - enemy.GetComponent<StatsController>().BaseStats.Speed) * 0.8f;
        }
        
        enemy.GetComponent<StatsController>().AddBonus(stats);
    }

    private int GetRandomLevel()
    {
        int minAvailableLevel = Mathf.Max(1, _currentEnemyLevel - RandomizingRange);
        int maxAvailableLevel = _currentEnemyLevel + RandomizingRange;
        return Random.Range(minAvailableLevel, maxAvailableLevel + 1);
    }

    private Color GetColorForLevel(int level)
    {
        int colorIndex = Math.Min(AvailableColors.Count, (level - 1) / AvailableIcons.Count);
        return AvailableColors[colorIndex];
    }

    private Texture GetIconForLevel(int level)
    {
        int colorIndex = (level - 1) % AvailableIcons.Count;
        return AvailableIcons[colorIndex];
    }

    private UnitStats GetStatsForLevel(int level)
    {
        return level * StatsDiffPerLevel;
    }

    private IEnumerator IncreaseEnemyLevel()
    {
        while (true)
        {
            yield return new WaitForSeconds(EnemyLevelChangeInterval);
            _currentEnemyLevel += 1;
        }
    }
}
