using UnityEngine;

[CreateAssetMenu(fileName = "GameConfigurationData", menuName = "ScriptableObjects/Game Configuration")]
public class GameConfigurationData : ScriptableObject
{
    [Range(0, 20)] public int lives = 0;
}
