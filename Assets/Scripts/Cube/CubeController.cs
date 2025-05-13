using System;
using UnityEngine;

namespace RaccoonsGames.Cube
{
    public interface ICubeController
    {
        event Action<ICubeController> OnLaunched;
        void Init(CubeView view);
        void SetValue(int newValue);
        void SetColor(Color color);
        void ResetImpulse();
        void PlayBounceEffect();
    }

    public class CubeController : ICubeController
    {
        public CubeModel Model { get; private set; }
        public CubeView View { get; private set; }

        public event Action<ICubeController> OnLaunched;

        private const float kMinPositionX = -2.4f;
        private const float kMaxPositionX = 2.4f;
        private const float KMoveSpeed = 0.005f;
        private const float kLaunchForce = 30f;

        private bool _isDragging;
        private Transform _transform;
        private Rigidbody _rigidbody;
        private CubeDragHandler _dragHandler;

        public CubeController(CubeModel model)
        {
            Model = model;
        }

        public void Init(CubeView view)
        {
            View = view;

            _transform = View.transform;
            _rigidbody = View.Rigidbody;
            _dragHandler = View.DragHandler;

            View.UpdateValue(Model.Value);
            SetColorBasedOnValue();

            _dragHandler.OnDragDelta += HandleDrag;
            _dragHandler.OnDragEnd += HandleDragEnd;
        }

        public void SetValue(int newValue)
        {
            Model.SetValue(newValue);
            View.UpdateValue(newValue);
            SetColorBasedOnValue();
        }

        public void SetColor(Color color)
        {
            View.UpdateColorCube(color);
        }

        public void PlayBounceEffect()
        {
            _rigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }

        public void ResetImpulse()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        private void SetColorBasedOnValue()
        {
            Color color = Model.GetColorForValue();
            View.UpdateColorCube(color);
        }

        private void HandleDrag(Vector2 delta)
        {
            if (!_isDragging)
            {
                _rigidbody.isKinematic = true;
                _isDragging = true;
            }

            float xMove = delta.x * KMoveSpeed;
            float newX = Mathf.Clamp(_transform.position.x + xMove, kMinPositionX, kMaxPositionX);

            _transform.position = new Vector3(newX, _transform.position.y, _transform.position.z);
        }

        private void HandleDragEnd()
        {
            _dragHandler.OnDragDelta -= HandleDrag;
            _dragHandler.OnDragEnd -= HandleDragEnd;

            if (_isDragging)
            {
                _isDragging = false;
                _rigidbody.isKinematic = false;

                _rigidbody.AddForce(Vector3.forward * kLaunchForce, ForceMode.Impulse);
                OnLaunched?.Invoke(this);
            }
        }
    }
}