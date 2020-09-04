using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Asteroids.UI
{
    public class HudView : BaseView
    {
        [SerializeField] private SharedData sharedData;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI livesText;
        [SerializeField] private TextMeshProUGUI scoreText;

        private void OnEnable()
        {
            sharedData.SubscribeToLevel(OnLevelComplete);
            sharedData.SubscribeToScore(UpdateScore, true);
            sharedData.SubscribeToLives(UpdateLives, true);
        }

        private void OnDisable()
        {
            sharedData.UnsubscribeToLevel(OnLevelComplete);
            sharedData.UnSubscribeToScore(UpdateScore);
            sharedData.UnsubscribeToLives(UpdateLives);
        }

        public void OnLevelComplete(int level)
        {
            levelText.text = String.Format("Level {0}", level);
            levelText.DOFade(1f, 0f);
            levelText.gameObject.SetActive(true);
            levelText.DOFade(0f, 0.5f).SetLoops(5, LoopType.Yoyo)
            .OnComplete(() => levelText.gameObject.SetActive(false));
        }

        private void UpdateScore(int score) => scoreText.text = score.ToString();

        private void UpdateLives(int lives) => livesText.text = String.Format("{0} x ", lives);
    }
}
