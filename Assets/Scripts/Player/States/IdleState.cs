using System;
using Core;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.States
{
    public sealed class IdleState : PlayerState
    {
        public IdleState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

        public static event Action<IdleState> Started;

        public static event Action<IdleState> Ended;

        public override void OnStart()
        {
            GameplayInput.OnMove += OnMoveInput;

            Player.Rigidbody.velocity = Vector2.zero;

            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            GameplayInput.OnMove -= OnMoveInput;
            Ended?.Invoke(this);
        }

        private void OnMoveInput(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            Player.Direction = Mathf.Round(context.ReadValue<Vector2>().x);

            StateMachine.ChangeState(Player.MoveState);
        }

        public override string ToString() => nameof(IdleState);
    }
}
