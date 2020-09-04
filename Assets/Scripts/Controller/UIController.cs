using UnityEngine;

namespace Asteroids.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private BaseView menu;
        [SerializeField] private BaseView hud;
        [SerializeField] private BaseView gameOver;

        private void OnEnable()
        {
            GameEvents.OnGameStartEvent += OnGameStarted;
            GameEvents.OnGameOverEvent += OnGameOver;
        }

        private void OnDisable()
        {
            GameEvents.OnGameStartEvent -= OnGameStarted;
            GameEvents.OnGameOverEvent -= OnGameOver;
        }

        private void OnGameStarted()
        {
            menu.Hide();
            gameOver.Hide();
            hud.Show();
        }

        private void OnGameOver()
        {
            hud.Hide();
            gameOver.Show();
        }
    }
}
