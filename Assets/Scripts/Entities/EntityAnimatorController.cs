using Core;
using UnityEngine;

namespace Entities
{
    public class EntityAnimatorController : MonoBehaviour
    {
        [SerializeField] private GameObject animableEntity;

        private IAnimable _animableComponent;

        private void Awake()
        {
            if (!animableEntity) return;

            animableEntity.TryGetComponent(out _animableComponent);
        }

        public void ChangeState(string state)
        {
            _animableComponent?.ChangeState(state);
        }

        public void FaceDirection(float direction)
        {
            _animableComponent?.FaceDirection(direction);
        }
    }
}
