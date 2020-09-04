using UnityEngine;

[CreateAssetMenu(fileName = "ScoringData", menuName = "ScriptableObjects/Scoring")]
public class ScoringData : ScriptableObject
{
    [Range(10, 1000)]
    public int largeAsteroidScore = 50;
    [Range(10, 1000)]
    public int smallAsteroidScore = 100;
    [Range(10, 1000)]
    public int saucerScore = 150;
}
