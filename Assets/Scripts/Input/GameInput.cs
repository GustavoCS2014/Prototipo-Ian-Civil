using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public sealed class GameInput : Input
    {
        public static event Action<InputAction.CallbackContext> PausePerformed;

        private static GameActions _gameActions;

        private void Awake()
        {
            _gameActions = new GameActions();
        }

        private void OnEnable()
        {
            _gameActions.General.Enable();

            _gameActions.General.Pause.performed += PauseAction;
        }

        private void OnDisable()
        {
            _gameActions.General.Disable();

            _gameActions.General.Pause.performed -= PauseAction;
        }

        private static void PauseAction(InputAction.CallbackContext context)
        {
            CurrentControlScheme = context.control.device.name == "Keyboard" ? ControlScheme.Keyboard : ControlScheme.Gamepad;
            Debug.Log(CurrentControlScheme);
            PausePerformed?.Invoke(context);
        }
    }
}
