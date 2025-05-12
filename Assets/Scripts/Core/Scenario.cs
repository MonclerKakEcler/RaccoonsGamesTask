using UnityEngine;
using Cysharp.Threading.Tasks;
using RaccoonsGames.Cube;
using RaccoonsGames.GameProgressUI;

namespace RaccoonsGames.Core
{
    public interface IScenario
    {
        void Init(Transform transform);
        void ResetGame();
    }

    public class Scenario : IScenario
    {
        private readonly CubePool _cubePool;
        private readonly ICubeService _cubeService;
        private readonly IGameProgressonController _gameProgressonController;

        private Transform _cubeSpawnPoint;

        public Scenario(CubePool cubePool,
            ICubeService cubeService,
            IGameProgressonController gameProgressonController)
        {
            _cubePool = cubePool;
            _cubeService = cubeService;
            _gameProgressonController = gameProgressonController;
        }

        public void Init(Transform transform)
        {
            _cubeSpawnPoint = transform;

            ActivateNewCube();
        }

        public void ResetGame()
        {
            _cubePool.ResetPoolForRestartGame();
            _gameProgressonController.ResetGameProgress();

            ActivateNewCube();
        }

        private void ActivateNewCube()
        {
            var value = GetRandomValue();

            var cubeView = _cubePool.Get(_cubeSpawnPoint.position, value);
            _cubeService.RegisterCube(cubeView);

            var controller = _cubePool.GetController(cubeView);
            controller.OnLaunched += OnCubeLaunched;
        }

        private async void OnCubeLaunched(ICubeController launched)
        {
            launched.OnLaunched -= OnCubeLaunched;

            await UniTask.Delay(500);
            ActivateNewCube();
        }

        private int GetRandomValue()
        {
            return Random.value < 0.25f ? 4 : 2;
        }
    }
}