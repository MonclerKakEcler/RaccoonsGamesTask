using System;

namespace RaccoonsGames.Cube
{
    public interface ICubeService
    {
        event Action OnAttemptUsed;
        event Action<int> OnCubesMerged;

        void RegisterCube(CubeView view);
    }

    public class CubeService : ICubeService
    {
        public event Action OnAttemptUsed;
        public event Action<int> OnCubesMerged;

        private CubePool _pool;

        public CubeService(CubePool pool)
        {
            _pool = pool;
        }

        public void RegisterCube(CubeView view)
        {
            view.OnMergeTry += TryMergeCubes;
            OnAttemptUsed?.Invoke();
        }

        private void TryMergeCubes(CubeView viewA, CubeView viewB, float speed)
        {
            if (speed < 2f)
            {
                return;
            }

            var controllerA = _pool.GetController(viewA);
            var controllerB = _pool.GetController(viewB);

            if (controllerA == null || controllerB == null) return;
            if (controllerA.Model.Value != controllerB.Model.Value) return;

            var remaining = controllerA;
            var toRemove = controllerB;

            int valueA = remaining.Model.Value;
            int valueB = toRemove.Model.Value;
            int result = CalculateNewValue(valueA);

            remaining.SetValue(result);
            remaining.PlayBounceEffect();
            toRemove.ResetImpulse();
            _pool.Release(toRemove.View);

            int score = result / 2;
            OnCubesMerged?.Invoke(score);
        }

        private int CalculateNewValue(int value) => value * 2;
    }
}