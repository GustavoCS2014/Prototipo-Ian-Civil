using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public sealed class GameplayInput : Input
    {
        #region Events

        public static event Action<InputAction.CallbackContext> OnJump;
        public static event Action<InputAction.CallbackContext> OnMove;
        public static event Action<InputAction.CallbackContext> OnShoot;

        #endregion

        public static Vector2 MoveDirection => _playerActions.Ground.Move.ReadValue<Vector2>();

        private static GameActions _playerActions;

        private void Awake()
        {
            _playerActions = new GameActions();
        }

        private void OnEnable()
        {
            _playerActions.Ground.Enable();

            _playerActions.Ground.Jump.performed += JumpAction;
            _playerActions.Ground.Jump.canceled += JumpAction;

            _playerActions.Ground.Move.performed += MoveAction;
            _playerActions.Ground.Move.canceled += MoveAction;

            _playerActions.Ground.Shoot.performed += ShootAction;
            _playerActions.Ground.Shoot.canceled += ShootAction;
        }

        private void OnDisable()
        {
            _playerActions.Ground.Disable();

            _playerActions.Ground.Jump.performed -= JumpAction;
            _playerActions.Ground.Jump.canceled -= JumpAction;

            _playerActions.Ground.Move.performed -= MoveAction;
            _playerActions.Ground.Move.canceled -= MoveAction;

            _playerActions.Ground.Shoot.performed -= ShootAction;
            _playerActions.Ground.Shoot.canceled -= ShootAction;
        }

        private static void JumpAction(InputAction.CallbackContext context)
        {
            OnJump?.Invoke(context);
        }

        private static void MoveAction(InputAction.CallbackContext context)
        {
            OnMove?.Invoke(context);
        }

        private static void ShootAction(InputAction.CallbackContext context)
        {
            OnShoot?.Invoke(context);
        }
    }
}
