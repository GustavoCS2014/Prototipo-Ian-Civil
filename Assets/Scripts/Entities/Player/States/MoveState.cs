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
            // if(context.performed){
            inputDirection = context.ReadValue<Vector2>();
            Owner.Direction = Mathf.Round(context.ReadValue<Vector2>().x);
            Debug.Log($"Inputs {inputDirection}, MDir {GameplayInput.MoveDirection}, Cntx {context.phase}");   
            
            // }
            if (!context.canceled) return;

            StateMachine.ChangeState(Owner.IdleState);
        }

        public override void Update()
        {
            base.Update();
            Debug.DrawRay(Vector3.zero, GameplayInput.MoveDirection, Color.red);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(Owner.OnStairs){
                Owner.Velocity = new Vector2(inputDirection.x * Owner.Settings.Speed, inputDirection.y * Owner.Settings.Speed);
                return;
            }
            Vector2 directionVector = Owner.VerifyDirectionVector();
            Owner.Velocity = Owner.Direction * Owner.Settings.Speed * directionVector;
        }

        public override string ToString() => nameof(MoveState);
    }
}
