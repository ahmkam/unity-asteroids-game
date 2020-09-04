using TMPro;
using UnityEngine;

namespace Asteroids.UI
{
    public class GameOverView : BaseView
    {
        [SerializeField] private SharedData sharedData;

        [SerializeField] private TextMeshProUGUI scoreText;

        private void OnEnable() => sharedData.SubscribeToScore(UpdateScore, true);

        private void OnDisable() => sharedData.UnSubscribeToScore(UpdateScore);

        private void UpdateScore(int score) => scoreText.text = score.ToString();
    }
}
