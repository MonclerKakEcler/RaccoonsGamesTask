using Zenject;
using UnityEngine;
using RaccoonsGames.GameProgressUI;

namespace RaccoonsGames.Core
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameProgressView _scoreView;

        public override void InstallBindings()
        {
            BindScore();
        }

        private void BindScore()
        {
            Container.Bind<GameProgressView>().FromInstance(_scoreView).AsSingle();
            var controller = Container.Resolve<IGameProgressonController>();
            controller.Init(_scoreView);
        }
    }
}
