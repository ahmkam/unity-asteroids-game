using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnerConfigurationData", menuName = "ScriptableObjects/Enemy Spawner Configuration")]
public class EnemySpawnerConfigurationData : ScriptableObject
{
    [Range(1, 5)]
    public int startingAsteroidCount = 1;
    [Range(1, 5)]
    public int asteroidsIncrementPerLevel = 1;
    [Range(2, 10)]
    public int smallAsteroidsToSpawnOnLargeDestroyed = 2;
    [Range(10, 1000)]
    public int largeAsteroidScore = 50;
    [Range(10, 1000)]
    public int smallAsteroidScore = 100;
    [Range(10, 1000)]
    public int saucerScore = 150;
    [Range(5, 50)]
    public int spawnRadius = 12;
    [Range(0.2f, 0.75f)]
    public float saucerSpawnProbability = 0.5f;
}
