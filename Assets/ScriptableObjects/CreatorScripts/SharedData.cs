using System;
using Asteroids.DataType;
using UnityEngine;

[CreateAssetMenu(fileName = "SharedData", menuName = "ScriptableObjects/Shared Data")]
public class SharedData : ScriptableObject
{
    [SerializeField] private GameConfigurationData configurationData;
    [SerializeField] private ReactiveInt score;
    [SerializeField] private ReactiveInt level;
    [SerializeField] private ReactiveInt lives;
    [SerializeField] private ReactiveBool gameOver;

    public void Reset()
    {
        score.Value = 0;
        level.Value = 1;
        lives.Value = configurationData.lives;
        gameOver.Value = false;
    }

    public void SubscribeToScore(Action<int> callback, bool forceUpdate = false) => score.Subscribe(callback, forceUpdate);

    public void SubscribeToLevel(Action<int> callback, bool forceUpdate = false) => level.Subscribe(callback, forceUpdate);

    public void SubscribeToLives(Action<int> callback, bool forceUpdate = false) => lives.Subscribe(callback, forceUpdate);

    public void SubscribeToGameOver(Action<bool> callback, bool forceUpdate = false) => gameOver.Subscribe(callback, forceUpdate);

    public void UnSubscribeToScore(Action<int> callback) => score.Unsubscribe(callback);

    public void UnsubscribeToLevel(Action<int> callback) => level.Unsubscribe(callback);

    public void UnsubscribeToLives(Action<int> callback) => lives.Unsubscribe(callback);

    public void UnsubscribeToGameOver(Action<bool> callback) => gameOver.Unsubscribe(callback);

    public void AddScore(int amount) => score.Value += amount;

    public int CurrentLevel => level.Value;

    public void IncrementLevel() => level.Value++;

    public void DecrementLives() => lives.Value--;

    public int CurrentLives => lives.Value;

    public void SetGameOver(bool isGameOver) => gameOver.Value = isGameOver;

    public bool IsGameOver => gameOver.Value;
}
