using System.Collections;
using Asteroids.DataType;
using Asteroids.Utils;
using UnityEngine;

namespace Asteroids
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private SharedData sharedData;
        [SerializeField] private ObjectPool objectPool;
        private const float CoroutinesStartDelay = 1f;
        private const int StartingLives = 1;

        void Start()
        {
            objectPool.Initialize();
            StartCoroutine(StartGameRoutine());
        }

        private void OnEnable() => GameEvents.OnGameOverEvent += OnGameOver;

        private void OnDisable() => GameEvents.OnGameOverEvent -= OnGameOver;

        private IEnumerator StartGameRoutine()
        {
            yield return new WaitForSeconds(CoroutinesStartDelay);
            yield return new WaitWhile(() => !Input.GetKeyDown(KeyCode.Return));
            ResetData();
            GameEvents.OnGameStartEvent?.Invoke();
        }

        private IEnumerator GameOverRoutine()
        {
            yield return new WaitForSeconds(CoroutinesStartDelay);
            yield return new WaitWhile(() => !Input.GetKeyDown(KeyCode.Return));
            ResetData();
            GameEvents.OnGameStartEvent?.Invoke();
        }

        private void OnGameOver() => StartCoroutine(GameOverRoutine());

        private void ResetData() => sharedData.Reset();
    }
}