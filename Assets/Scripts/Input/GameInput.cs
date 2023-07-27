using System;
using Core;
using Management;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Input
{
    public abstract class GameInput : MonoBehaviour
    {
        private const string GameActionsPath = "Assets/Settings/GameActions.inputactions";

        public static event Action<ControlScheme> ControlSchemeChanged;

        public static string CurrentDevice { get; private set; }

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

        private static InputActionAsset _actionsAsset;

        public static InputActionAsset ActionsAsset
        {
            get
            {
                if (_actionsAsset) return _actionsAsset;
                _actionsAsset = AssetDatabase.LoadAssetAtPath<InputActionAsset>(GameActionsPath);
                return _actionsAsset;
            }
        }

        private static ControlScheme _currentControlScheme;

        [SerializeField] protected GameState enabledInStates;

        protected virtual void Awake()
        {
            GameManager.StateChanged += OnGameStateChanged;
            InputSystem.onAnyButtonPress.Call(OnAnyButtonPress);
        }

        private void OnDestroy()
        {
            GameManager.StateChanged -= OnGameStateChanged;
        }

        private static void OnAnyButtonPress(InputControl control)
        {
            CurrentDevice = control.device.name;

            CurrentControlScheme = CurrentDevice switch
            {
                "Keyboard" or "Mouse" => ControlScheme.Keyboard,
                "AndroidGamepad" => ControlScheme.AndroidGamepad,
                "PlayStation" => ControlScheme.PLayStation,
                "Switch" => ControlScheme.Switch,
                "WebGLGamepad" => ControlScheme.WebGLGamepad,
                "Xbox" => ControlScheme.Xbox,
                _ => ControlScheme.Gamepad
            };
        }

        private void OnGameStateChanged(GameState state)
        {
            enabled = (enabledInStates & state) == state;
        }
    }
}
