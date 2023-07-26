using System;
using UnityEngine.InputSystem;

namespace Input
{
    public sealed class GameInput : Input
    {
        public static event Action<InputAction.CallbackContext> PausePerformed;
        public static event Action<InputAction.CallbackContext> SkipPressed;
        public static event Action<InputAction.CallbackContext> SkipReleased;

        private static GameActions _gameActions;

        protected override void Awake()
        {
            base.Awake();
            _gameActions = new GameActions();
        }

        private void OnEnable()
        {
            _gameActions.General.Enable();

            _gameActions.General.Pause.performed += PauseAction;
            _gameActions.General.Skip.performed += SkipAction;
            _gameActions.General.Skip.canceled += SkipAction;
        }

        private void OnDisable()
        {
            _gameActions.General.Disable();

            _gameActions.General.Pause.performed -= PauseAction;
            _gameActions.General.Skip.performed -= SkipAction;
            _gameActions.General.Skip.canceled -= SkipAction;
        }

        private static void PauseAction(InputAction.CallbackContext context)
        {
            SetCurrentControlScheme(context);
            PausePerformed?.Invoke(context);
        }

        private static void SkipAction(InputAction.CallbackContext context)
        {
            SetCurrentControlScheme(context);
            if (context.performed)
                SkipPressed?.Invoke(context);
            else if (context.canceled)
                SkipReleased?.Invoke(context);
        }
    }
}
