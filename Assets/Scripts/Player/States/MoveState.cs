using System;
using Core;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.States
{
    public sealed class MoveState : PlayerState
    {
        public MoveState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

        public static event Action<MoveState> Started;

        public static event Action<MoveState> Ended;

        public override void OnStart()
        {
            GameplayInput.OnMove += OnMoveInput;
            GameplayInput.OnJump += OnJumpInput;
            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            GameplayInput.OnMove -= OnMoveInput;
            GameplayInput.OnJump -= OnJumpInput;
            Ended?.Invoke(this);
        }

        private void OnMoveInput(InputAction.CallbackContext context)
        {
            if (!context.canceled) return;

            Player.Direction = 0f;

            StateMachine.ChangeState(Player.IdleState);
        }

        private void OnJumpInput(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            if (!Player.Grounded) return;

            StateMachine.ChangeState(Player.JumpState);
        }

        public override void FixedUpdate()
        {
            Player.Rigidbody.velocity = Player.Direction * Player.Settings.Speed * Vector2.right;
        }

        public override string ToString() => nameof(MoveState);
    }
}
