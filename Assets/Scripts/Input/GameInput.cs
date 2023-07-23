using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public sealed class GameInput : MonoBehaviour
    {
        public static event Action<InputAction.CallbackContext> PausePerformed;

        private GameActions _playerActions;

        private void Awake()
        {
            _playerActions = new GameActions();
        }

        private void OnEnable()
        {
            _playerActions.UI.Enable();

            _playerActions.UI.Pause.performed += PauseAction;
        }

        private void OnDisable()
        {
            _playerActions.UI.Disable();

            _playerActions.UI.Pause.performed -= PauseAction;
        }

        private static void PauseAction(InputAction.CallbackContext context)
        {
            PausePerformed?.Invoke(context);
        }
    }
}
