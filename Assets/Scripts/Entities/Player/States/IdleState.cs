using System;
using Core;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player.States
{
    public sealed class IdleState : GroundState
    {
        public IdleState(PlayerController player, StateMachine<PlayerController> stateMachine) : base(player, stateMachine) { }

        public static event Action<IdleState> Started;

        public static event Action<IdleState> Ended;

        public override void OnStart()
        {
            base.OnStart();
            GameplayInput.OnMove += OnMoveInput;

            Owner.Velocity = Vector2.zero;

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
            if (!context.performed) return;

            Owner.Direction = Mathf.Round(context.ReadValue<Vector2>().x);

            StateMachine.ChangeState(Owner.MoveState);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            Owner.Velocity = new Vector2
            {
                x = 0f,
                y = Owner.Velocity.y
            };
        }

        public override string ToString() => nameof(IdleState);
    }
}
