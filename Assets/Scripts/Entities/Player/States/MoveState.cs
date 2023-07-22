using System;
using Core;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player.States
{
    public sealed class MoveState : GroundState
    {
        public MoveState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

        public static event Action<MoveState> Started;

        public static event Action<MoveState> Ended;

        public override void OnStart()
        {
            base.OnStart();
            GameplayInput.OnMove += OnMoveInput;
            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            base.OnEnd();
            GameplayInput.OnMove -= OnMoveInput;
            Ended?.Invoke(this);
        }

        private void OnMoveInput(InputAction.CallbackContext context)
        {
            Player.Direction = Mathf.Round(context.ReadValue<Vector2>().x);

            if (!context.canceled) return;

            StateMachine.ChangeState(Player.IdleState);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Player.Rigidbody.velocity = Player.Direction * Player.Settings.Speed * Vector2.right;
        }

        public override string ToString() => nameof(MoveState);
    }
}
