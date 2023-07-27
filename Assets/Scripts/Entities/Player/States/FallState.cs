using System;
using Core;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player.States
{
    public sealed class FallState : State<PlayerController>
    {
        public FallState(PlayerController player, StateMachine<PlayerController> stateMachine) : base(player, stateMachine) { }

        public static event Action<FallState> Started;

        public static event Action<FallState> Ended;

        public override void OnStart()
        {
            GameplayInput.OnMove += OnMoveInput;
            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            GameplayInput.OnMove -= OnMoveInput;
            Ended?.Invoke(this);
        }

        private void OnMoveInput(InputAction.CallbackContext context)
        {
            Owner.Direction = Mathf.Round(context.ReadValue<Vector2>().x);
        }

        public override void FixedUpdate()
        {
            Owner.Rigidbody.velocity = new Vector2
            {
                x = Owner.Direction * Owner.Settings.Speed * Owner.Settings.AirInputInfluence,
                y = Owner.Rigidbody.velocity.y
            };

            if (Owner.Grounded)
                StateMachine.ChangeState(GameplayInput.MoveDirection.x != 0f ? Owner.MoveState : Owner.IdleState);
        }

        public override string ToString() => nameof(FallState);
    }
}
