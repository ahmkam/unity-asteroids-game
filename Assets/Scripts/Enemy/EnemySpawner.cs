using UnityEngine;
using System.Collections;
using Asteroids.Utils;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Asteroids
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SharedData sharedData;
        [SerializeField] private EnemySpawnerConfigurationData configurationData;
        [SerializeField] private ObjectPool objectPool;
        [SerializeField] private AudioSource destroySfx;
        private int enemiesToDestroy;
        private bool saucerSpawnedCurrentLevel;
        private const float StartDelay = 2f;
        private const float LevelCompleteDelay = 2.5f;

        void OnEnable() => GameEvents.OnGameStartEvent += OnGameStarted;

        void OnDisable() => GameEvents.OnGameStartEvent -= OnGameStarted;

        private void OnGameStarted()
        {
            objectPool.ReturnAllEnemies();
            StartCoroutine(SpawnAsteroids(sharedData.CurrentLevel, StartDelay));
        }

        public IEnumerator SpawnAsteroids(int levelNumber, float startDelay)
        {
            yield return new WaitForSeconds(startDelay);

            int enemiesToSpawn = configurationData.startingAsteroidCount + (configurationData.asteroidsIncrementPerLevel * (levelNumber - 1));
            saucerSpawnedCurrentLevel = false;
            enemiesToDestroy = 0;
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnAsteroid(objectPool.GetLargeAsteroid(), RandomSpawnPosition());
                enemiesToDestroy = enemiesToDestroy + (configurationData.smallAsteroidsToSpawnOnLargeDestroyed + 1);
            }
        }

        public void OnEnemyDestroyed(BaseEnemy enemy)
        {
            destroySfx.Play();
            enemiesToDestroy--;
            enemy.ReturnToPool();

            if (enemy is Asteroid)
            {
                CheckSaucerSpawning();
                Asteroid asteroid = (Asteroid)enemy;
                if (asteroid.IsLarge)
                {
                    for (int i = 0; i < configurationData.smallAsteroidsToSpawnOnLargeDestroyed; i++)
                    {
                        SpawnAsteroid(objectPool.GetSmallAsteroid(), asteroid.transform.position);
                    }
                }
            }

            AddScore(enemy.DestroyScore);
            StartCoroutine(CheckLevelCompletionRoutine());
        }

        private void CheckSaucerSpawning()
        {
            if (!saucerSpawnedCurrentLevel)
            {
                bool spawnSaucer = Random.Range(1f, 100f) / 100f < configurationData.saucerSpawnProbability;
                if (spawnSaucer)
                {
                    enemiesToDestroy++;
                    saucerSpawnedCurrentLevel = true;
                    SpawnFlyingSaucer(RandomSpawnPosition());
                }
            }
        }

        private IEnumerator CheckLevelCompletionRoutine()
        {
            yield return null;

            if (!sharedData.IsGameOver)
            {
                if (enemiesToDestroy == 0)
                {
                    sharedData.IncrementLevel();
                    StartCoroutine(SpawnAsteroids(sharedData.CurrentLevel, LevelCompleteDelay));
                }
            }
        }

        private void SpawnFlyingSaucer(Vector2 position)
        {
            FlyingSaucer saucer = objectPool.GetFlyingSaucer();
            saucer.OnDestroyedEvent = OnEnemyDestroyed;
            saucer.SpawnFlyingSaucer(position, Random.insideUnitCircle.normalized);
        }

        private void SpawnAsteroid(Asteroid typeToSpawn, Vector2 position)
        {
            typeToSpawn.SpawnAsteroid(position, Random.insideUnitCircle.normalized);
            typeToSpawn.OnDestroyedEvent = OnEnemyDestroyed;
        }

        private void AddScore(int scoreToAdd) => sharedData.AddScore(scoreToAdd);

        private Vector2 RandomSpawnPosition() => Random.insideUnitCircle.normalized * configurationData.spawnRadius;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (configurationData)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(Vector3.zero, configurationData.spawnRadius);
            }
        }
#endif
    }
}