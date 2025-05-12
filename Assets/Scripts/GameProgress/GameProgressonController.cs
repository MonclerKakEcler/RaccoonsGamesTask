using RaccoonsGames.Cube;

namespace RaccoonsGames.GameProgressUI
{
    public interface IGameProgressonController
    {
        void Init(GameProgressView view);
        void ResetGameProgress();
    }
    public class GameProgressonController : IGameProgressonController
    {
        private GameProgressView _view;
        private GameProgressModel _model;
        private ICubeService _cubeService;

        public GameProgressonController(GameProgressModel model, ICubeService cubeService)
        {
            _model = model;
            _cubeService = cubeService;
        }

        public void Init(GameProgressView view)
        {
            _view = view;

            _view.UpdateScore(_model.Value);
            _view.UpdateAttemptCount(_model.AttemptCount);

            _cubeService.OnAttemptUsed += HandleAttempt;
            _cubeService.OnCubesMerged += HandleScore;
        }

        public void ResetGameProgress()
        {
            _model = new GameProgressModel();
            _view.UpdateScore(_model.Value);
            _view.UpdateAttemptCount(_model.AttemptCount);
        }

        private void HandleScore(int score)
        {
            _model.AddScore(score);
            _view.UpdateScore(_model.Value);

            if (_model.IsGoalReached())
            {
                _view.IsActiveWinScreen(true);
            }
        }

        private void HandleAttempt()
        {
            _model.DecrementAttempts();
            _view.UpdateAttemptCount(_model.AttemptCount);

            if (_model.IsOutOfAttempts())
            {
                _view.IsActiveLoseScreen(true);
            }
        }
    }
}