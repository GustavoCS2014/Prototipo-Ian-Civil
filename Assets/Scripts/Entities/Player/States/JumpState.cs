using System;
using Core;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Entities.Player.States
{
    public sealed class JumpState : State<PlayerController>
    {
        public JumpState(PlayerController player, StateMachine<PlayerController> stateMachine) : base(player, stateMachine) { }

        public static event Action<JumpState> Started;

        public static event Action<JumpState> Ended;

        public override void OnStart()
        {
            GameplayInput.OnMove += OnMoveInput;

            float force = PhysicsCalculator.JumpStrength(Owner.Rigidbody, Owner.Settings.JumpHeight);
            Owner.AddForce(Vector2.up * force, ForceMode2D.Impulse);

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
            Owner.Velocity = new Vector2
            {
                x = Owner.Direction * Owner.Settings.Speed * Owner.Settings.AirInputInfluence,
                y = Owner.Velocity.y
            };

            if (Owner.Velocity.y < 0f)
                StateMachine.ChangeState(Owner.FallState);
        }

        public override string ToString() => nameof(JumpState);
    }
}
