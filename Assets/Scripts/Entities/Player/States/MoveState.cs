using System;
using Core;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player.States
{
    public sealed class MoveState : GroundState
    {
        public MoveState(PlayerController player, StateMachine<PlayerController> stateMachine) : base(player, stateMachine) { }

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
            Owner.Direction = Mathf.Round(context.ReadValue<Vector2>().x);

            if (!context.canceled) return;

            StateMachine.ChangeState(Owner.IdleState);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Vector2 directionVector = Owner.GetDirectionVector();
            Owner.Velocity = Owner.Direction * Owner.Settings.Speed * directionVector;
        }

        public override string ToString() => nameof(MoveState);
    }
}
