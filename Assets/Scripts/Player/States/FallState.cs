using System;
using Core;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.States
{
    public sealed class FallState : PlayerState
    {
        public FallState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

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
            Player.Direction = Mathf.Round(context.ReadValue<Vector2>().x);
        }

        public override void FixedUpdate()
        {
            Player.Rigidbody.velocity = new Vector2
            {
                x = Player.Direction * Player.Settings.Speed * Player.Settings.AirInputInfluence,
                y = Player.Rigidbody.velocity.y
            };

            if (Player.Grounded)
                StateMachine.ChangeState(Player.Direction != 0f ? Player.MoveState : Player.IdleState);
        }

        public override string ToString() => nameof(FallState);
    }
}
