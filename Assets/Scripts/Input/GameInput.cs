using System;
using UnityEngine.InputSystem;

namespace Input
{
    public sealed class GameInput : Input
    {
        public static event Action<InputAction.CallbackContext> PausePerformed;

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
        }

        private void OnDisable()
        {
            _gameActions.General.Disable();

            _gameActions.General.Pause.performed -= PauseAction;
        }

        private static void PauseAction(InputAction.CallbackContext context)
        {
            SetCurrentControlScheme(context);
            PausePerformed?.Invoke(context);
        }
    }
}
