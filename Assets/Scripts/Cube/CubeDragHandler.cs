using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RaccoonsGames.Cube
{
    public interface ICubeDragHandler : IBeginDragHandler, IDragHandler, IEndDragHandler
    { }

    public class CubeDragHandler : MonoBehaviour, ICubeDragHandler
    {
        public event Action<Vector2> OnDragDelta;
        public event Action OnDragEnd;

        private Vector2 _lastPosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _lastPosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 delta = eventData.position - _lastPosition;
            _lastPosition = eventData.position;
            OnDragDelta?.Invoke(delta);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnDragEnd?.Invoke();
        }
    }
}
