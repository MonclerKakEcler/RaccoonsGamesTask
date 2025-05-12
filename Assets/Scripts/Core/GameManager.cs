using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RaccoonsGames.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Image _winScreen;
        [SerializeField] private Image _gameOverScreen;
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private Button _tryAgainButton;
        [SerializeField] private Transform _cubeSpawnPoint;

        [Inject] IScenario _scenario;

        private void OnEnable()
        {
            _playAgainButton.onClick.AddListener(ClickPlayAgain);
            _tryAgainButton.onClick.AddListener(ClickTryAgain);
        }

        private void Start()
        {
            _scenario.Init(_cubeSpawnPoint);
        }

        private void OnDisable()
        {
            _playAgainButton.onClick.RemoveListener(ClickPlayAgain);
            _tryAgainButton.onClick.RemoveListener(ClickTryAgain);
        }

        private void ClickPlayAgain()
        {
            _winScreen.gameObject.SetActive(false);
            _scenario.ResetGame();
        }

        private void ClickTryAgain()
        {
            _gameOverScreen.gameObject.SetActive(false);
            _scenario.ResetGame();
        }
    }
}
