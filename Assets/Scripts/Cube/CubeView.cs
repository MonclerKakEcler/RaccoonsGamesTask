using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

namespace RaccoonsGames.Cube
{
    public class CubeView : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private List<TMP_Text> _numberTexts;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CubeDragHandler _dragHandler;

        public event Action<CubeView, CubeView, float> OnMergeTry;
        public Transform Transform => _transform;
        public CubeDragHandler DragHandler => _dragHandler;
        public Rigidbody Rigidbody => _rigidbody;

        public void UpdateValue(int value)
        {
            for (int i = 0; i < _numberTexts.Count; i++)
            {
                _numberTexts[i].text = value.ToString();
            }
        }

        public void UpdateColorCube(Color color)
        {
            _renderer.material.color = color;
        }

        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.TryGetComponent(out CubeView other))
            {
                float impactForce = collision.relativeVelocity.magnitude;
                OnMergeTry?.Invoke(this, other, impactForce);
            }
        }
    }
}