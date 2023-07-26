using System;
using Core;
using Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Cinematics
{
    public class CutsceneSkipper : MonoBehaviour, IHasProgress
    {
        public event Action<float> ProgressUpdated;

        [SerializeField] private float pressTime;
        [SerializeField] private UnityEvent onProgressCompleted;

        private float _timer;
        private bool _isProgressing;

        public float Progress => _timer / pressTime;

        private void Awake()
        {
            GameInput.SkipPressed += OnSkipPressed;
            GameInput.SkipReleased += OnSkipReleased;
        }

        private void Update()
        {
            if (!_isProgressing)
                return;

            _timer += Time.deltaTime;
            if (_timer >= pressTime)
            {
                _isProgressing = false;
                _timer = 0f;
                ProgressUpdated?.Invoke(1f);
                onProgressCompleted?.Invoke();
            }
            else
            {
                ProgressUpdated?.Invoke(Progress);
            }
        }

        private void OnDestroy()
        {
            GameInput.SkipPressed -= OnSkipPressed;
            GameInput.SkipReleased -= OnSkipReleased;
        }

        private void OnSkipPressed(InputAction.CallbackContext context)
        {
            _isProgressing = true;
            _timer = 0f;
            ProgressUpdated?.Invoke(0f);
        }

        private void OnSkipReleased(InputAction.CallbackContext context)
        {
            _isProgressing = false;
            _timer = 0f;
            ProgressUpdated?.Invoke(0f);
        }
    }
}
