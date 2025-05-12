using Zenject;
using UnityEngine;
using RaccoonsGames.Cube;
using RaccoonsGames.GameProgressUI;

namespace RaccoonsGames.Core
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private CubeView _cubePrefab;

        public override void InstallBindings()
        {
            BindCube();
            BindScore();

            Container.Bind<IScenario>().To<Scenario>().AsSingle();
        }

        private void BindCube()
        {
            Container.Bind<CubeView>().FromInstance(_cubePrefab).AsTransient();
            Container.Bind<ICubeController>().To<CubeController>().AsTransient();
            Container.Bind<CubeModel>().To<CubeModel>().AsTransient();
            Container.Bind<CubePool>().To<CubePool>().AsSingle();
            Container.Bind<ICubeService>().To<CubeService>().AsSingle();
        }

        private void BindScore()
        {
            Container.Bind<IGameProgressonController>().To<GameProgressonController>().AsSingle();
            Container.Bind<GameProgressModel>().To<GameProgressModel>().AsSingle();
        }
    }
}
