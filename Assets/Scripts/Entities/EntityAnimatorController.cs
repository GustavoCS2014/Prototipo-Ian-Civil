using Core;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class EntityAnimatorController : MonoBehaviour
    {
        [SerializeField] private GameObject animableEntity;
        [SerializeField] private UnityEvent onStartAnimating;
        [SerializeField] private UnityEvent onStopAnimating;

        private IAnimable _animableComponent;

        private void Awake()
        {
            if (!animableEntity) return;

            if (!animableEntity.TryGetComponent(out _animableComponent))
                Debug.Log($"{animableEntity.name} does not have an <b>IAnimable</b> component", this);
        }

        public void ChangeState(string state)
        {
            _animableComponent?.ChangeState(state);
        }

        public void FaceDirection(float direction)
        {
            _animableComponent?.FaceDirection(direction);
        }

        public void StartAnimating()
        {
            if (_animableComponent is null) return;

            _animableComponent.Animating = true;
            onStartAnimating?.Invoke();
        }

        public void StopAnimating()
        {
            if (_animableComponent is null) return;

            _animableComponent.Animating = false;
            onStopAnimating?.Invoke();
        }
    }
}
