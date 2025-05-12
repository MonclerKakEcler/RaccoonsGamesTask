using TMPro;
using UnityEngine;

namespace RaccoonsGames.GameProgressUI
{
    public class GameProgressView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _attemptCountText;
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _loseScreen;

        private const string kScore = "Score: ";
        private const string kAttempts = "Attempts: ";

        public void UpdateScore(int score)
        {
            _scoreText.text = kScore + score.ToString();
        }

        public void UpdateAttemptCount(int attemptCount)
        {
            _attemptCountText.text = kAttempts + attemptCount.ToString();
        }

        public void IsActiveWinScreen(bool isActive)
        {
            _winScreen.SetActive(isActive);
        }

        public void IsActiveLoseScreen(bool isActive)
        {
            _loseScreen.SetActive(isActive);
        }
    }
}