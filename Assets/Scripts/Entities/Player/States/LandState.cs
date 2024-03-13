using System;
using Core;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player.States
{
    public sealed class LandState : State<PlayerController>
    {
        private float _landTime;

        public LandState(PlayerController owner, StateMachine<PlayerController> stateMachine) : base(owner, stateMachine){ }

        public static event Action<LandState> Started;

        public static event Action<LandState> Ended;

        public override void OnStart()
        {
            GameplayInput.OnJump += OnJumpInput;
            GameplayInput.OnMove += OnMoveInput;

            _landTime = Time.time + Owner.Settings.LandAnimationTime;

            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            GameplayInput.OnJump -= OnJumpInput;
            GameplayInput.OnMove -= OnMoveInput;
            Ended?.Invoke(this);
        }

        private void OnJumpInput(InputAction.CallbackContext context)
        {
            if (context.performed && !Owner.OnStairs)
                StateMachine.ChangeState(Owner.JumpState);
        }

        private void OnMoveInput(InputAction.CallbackContext context)
        {
            Owner.Direction = Mathf.Round(context.ReadValue<Vector2>().x);
        }

        public override void Update()
        {
            if (Time.time >= _landTime)
                StateMachine.ChangeState(GameplayInput.MoveDirection.x != 0f ? Owner.MoveState : Owner.IdleState);
        }
    }
}
