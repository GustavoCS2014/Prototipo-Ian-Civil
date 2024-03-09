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

        private Vector2 inputDirection;

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
            inputDirection = context.ReadValue<Vector2>();
            Debug.Log($"Inputs {inputDirection}");   
            if (!context.canceled) return;

            StateMachine.ChangeState(Owner.IdleState);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(Owner.OnStairs){
                Debug.Log($"Owner Velocity {Owner.Velocity.x}, {Owner.Velocity.y}, On Stairs? {Owner.OnStairs}"); 
                Owner.Velocity = new Vector2(inputDirection.x * Owner.Settings.Speed, inputDirection.y * Owner.Settings.Speed);
                return;
            }
            Vector2 directionVector = Owner.VerifyDirectionVector();
            Owner.Velocity = Owner.Direction * Owner.Settings.Speed * directionVector;
        }

        public override string ToString() => nameof(MoveState);
    }
}
