using System;
using Core;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player.States
{
    public sealed class JumpState : PlayerState
    {
        public JumpState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

        public static event Action<PlayerState> Started;

        public static event Action<PlayerState> Ended;

        public override void OnStart()
        {
            GameplayInput.OnMove += OnMoveInput;

            float force = GetJumpForce();
            Player.Rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);

            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            GameplayInput.OnMove -= OnMoveInput;
            Ended?.Invoke(this);
        }

        private void OnMoveInput(InputAction.CallbackContext context)
        {
            Player.Direction = Mathf.Round(context.ReadValue<Vector2>().x);
        }

        public override void FixedUpdate()
        {
            Player.Rigidbody.velocity = new Vector2
            {
                x = Player.Direction * Player.Settings.Speed * Player.Settings.AirInputInfluence,
                y = Player.Rigidbody.velocity.y
            };

            if (Player.Rigidbody.velocity.y < 0f)
                StateMachine.ChangeState(Player.FallState);
        }

        private float GetJumpForce()
        {
            float gravityScaled = Physics2D.gravity.y * Player.Rigidbody.gravityScale;
            float height = Player.Settings.JumpHeight;
            float mass = Player.Rigidbody.mass;

            return Mathf.Sqrt(-2f * gravityScaled * height) * mass;
        }

        public override string ToString() => nameof(JumpState);
    }
}
