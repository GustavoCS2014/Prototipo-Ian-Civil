using System;
using Core;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player.States
{
    public sealed class JumpState : State<PlayerController>
    {
        public JumpState(PlayerController player, StateMachine<PlayerController> stateMachine) : base(player, stateMachine) { }

        public static event Action<State<PlayerController>> Started;

        public static event Action<State<PlayerController>> Ended;

        public override void OnStart()
        {
            GameplayInput.OnMove += OnMoveInput;

            float force = GetJumpForce();
            Owner.Rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);

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

            if (Owner.Rigidbody.velocity.y < 0f)
                StateMachine.ChangeState(Owner.FallState);
        }

        private float GetJumpForce()
        {
            float gravityScaled = Physics2D.gravity.y * Owner.Rigidbody.gravityScale;
            float height = Owner.Settings.JumpHeight;
            float mass = Owner.Rigidbody.mass;

            return Mathf.Sqrt(-2f * gravityScaled * height) * mass;
        }

        public override string ToString() => nameof(JumpState);
    }
}
