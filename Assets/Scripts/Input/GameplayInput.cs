using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public sealed class GameplayInput : MonoBehaviour
    {
        #region Events

        public static event Action<InputAction.CallbackContext> OnJump;
        public static event Action<InputAction.CallbackContext> OnMove;
        public static event Action<InputAction.CallbackContext> OnShoot;

        #endregion

        private PlayerActions _playerActions;

        private void Awake()
        {
            _playerActions = new PlayerActions();
        }

        private void OnEnable()
        {
            _playerActions.Ground.Enable();

            _playerActions.Ground.Jump.performed += OnJump;
            _playerActions.Ground.Jump.canceled += OnJump;

            _playerActions.Ground.Move.performed += OnMove;
            _playerActions.Ground.Move.canceled += OnMove;

            _playerActions.Ground.Shoot.performed += OnShoot;
            _playerActions.Ground.Shoot.canceled += OnShoot;
        }

        private void OnDisable()
        {
            _playerActions.Ground.Disable();

            _playerActions.Ground.Jump.performed -= OnJump;
            _playerActions.Ground.Jump.canceled -= OnJump;

            _playerActions.Ground.Move.performed -= OnMove;
            _playerActions.Ground.Move.canceled -= OnMove;

            _playerActions.Ground.Shoot.performed -= OnShoot;
            _playerActions.Ground.Shoot.canceled -= OnShoot;
        }
    }
}
