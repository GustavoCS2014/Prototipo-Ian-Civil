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
            Owner.Direction = Mathf.Round(context.ReadValue<Vector2>().x);
        }

        private void OnJumpInput(InputAction.CallbackContext context){
            if(!Owner.IsOnCoyoteTime) return;
            if(Owner.OnStairs) return;
            StateMachine.ChangeState(Owner.JumpState);
        }

        public override void FixedUpdate()
        {
            Owner.Velocity = new Vector2
            {
                x = Owner.Direction * Owner.Settings.Speed * Owner.Settings.AirInputInfluence,
                y = Owner.Velocity.y
            };

            if (Owner.Grounded)
                StateMachine.ChangeState(Owner.LandState);
        }

        public override string ToString() => nameof(FallState);
    }
}
