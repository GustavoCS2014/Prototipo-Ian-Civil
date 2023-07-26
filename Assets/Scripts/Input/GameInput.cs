using System;
using Core;
using Management;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public abstract class GameInput : MonoBehaviour
    {
        public static event Action<ControlScheme> ControlSchemeChanged;

        public static ControlScheme CurrentControlScheme
        {
            get => _currentControlScheme;
            private set
            {
                if (_currentControlScheme == value) return;
                _currentControlScheme = value;
                ControlSchemeChanged?.Invoke(value);
            }
        }

        private static ControlScheme _currentControlScheme;

        [SerializeField] protected GameState enabledInStates;

        protected virtual void Awake()
        {
            GameManager.StateChanged += OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.StateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            enabled = (enabledInStates & state) == state;
        }

        protected static void SetCurrentControlScheme(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            CurrentControlScheme = context.control.device.name is "Keyboard" or "Mouse"
                ? ControlScheme.Keyboard
                : ControlScheme.Gamepad;
        }
    }
}
