using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public sealed class UIInput : Input
    {
        public static event Action<InputAction.CallbackContext> Navigate;
        public static event Action<InputAction.CallbackContext> SubmitPerformed;
        public static event Action<InputAction.CallbackContext> CancelPerformed;
        public static event Action<InputAction.CallbackContext> PointPerformed;

        public Vector2 Direction => _uiActions.UI.Navigate.ReadValue<Vector2>();

        private static GameActions _uiActions;

        protected override void Awake()
        {
            base.Awake();
            _uiActions = new GameActions();
        }

        private void OnEnable()
        {
            _uiActions.UI.Enable();

            _uiActions.UI.Navigate.performed += NavigateAction;
            _uiActions.UI.Submit.performed += SubmitAction;
            _uiActions.UI.Cancel.performed += CancelAction;
            _uiActions.UI.Point.performed += PointAction;
        }

        private void OnDisable()
        {
            _uiActions.UI.Disable();

            _uiActions.UI.Navigate.performed -= NavigateAction;
            _uiActions.UI.Submit.performed -= SubmitAction;
            _uiActions.UI.Cancel.performed -= CancelAction;
            _uiActions.UI.Point.performed -= PointAction;
        }

        private static void NavigateAction(InputAction.CallbackContext context)
        {
            SetCurrentControlScheme(context);
            Navigate?.Invoke(context);
        }

        private static void SubmitAction(InputAction.CallbackContext context)
        {
            SetCurrentControlScheme(context);
            SubmitPerformed?.Invoke(context);
        }

        private static void CancelAction(InputAction.CallbackContext context)
        {
            SetCurrentControlScheme(context);
            CancelPerformed?.Invoke(context);
        }

        private static void PointAction(InputAction.CallbackContext context)
        {
            SetCurrentControlScheme(context);
            PointPerformed?.Invoke(context);
        }
    }
}
