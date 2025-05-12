using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

namespace RaccoonsGames.Cube
{
    public class CubePool
    {
        private readonly ObjectPool<CubeView> _pool;
        private readonly CubeView _prefab;
        private readonly Transform _container;
        private readonly Dictionary<CubeView, CubeController> _controllers = new();

        public CubePool(CubeView prefab)
        {
            _prefab = prefab;

            GameObject poolParent = new GameObject("PoolCubes");
            _container = poolParent.transform;

            _pool = new ObjectPool<CubeView>(
                CreateCube,
                OnGet,
                OnRelease,
                OnDestroy,
                collectionCheck: true,
                defaultCapacity: 30,
                maxSize: 50
            );

            PrewarmPool(30);
        }

        public CubeView Get(Vector3 spawnPosition, int value = 0)
        {
            var view = _pool.Get();

            view.transform.position = spawnPosition;

            if (_controllers.TryGetValue(view, out var controller))
            {
                controller.Init(view);
                controller.SetValue(value);
            }

            return view;
        }

        public void Release(CubeView view)
        {
            _pool.Release(view);
        }

        public CubeController GetController(CubeView view)
        {
            return _controllers.TryGetValue(view, out var controller) ? controller : null;
        }

        public void ResetPoolForRestartGame(int minCount = 30)
        {
            foreach (var kvp in _controllers)
            {
                if (kvp.Key != null)
                    Object.Destroy(kvp.Key.gameObject);
            }

            _controllers.Clear();
            _pool.Clear();

            PrewarmPool(minCount);
        }

        private void PrewarmPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var view = CreateCube();
                _pool.Release(view);
            }
        }

        private CubeView CreateCube()
        {
            var view = Object.Instantiate(_prefab, _container);
            var model = new CubeModel();
            var controller = new CubeController(model);
            controller.Init(view);

            _controllers[view] = controller;
            view.gameObject.name = "CubeView";

            view.gameObject.SetActive(false);
            return view;
        }

        private void OnGet(CubeView view)
        {
            view.gameObject.SetActive(true);
        }

        private void OnRelease(CubeView view)
        {
            view.gameObject.SetActive(false);
        }

        private void OnDestroy(CubeView view)
        {
            Object.Destroy(view.gameObject);
            _controllers.Remove(view);
        }
    }
}