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
            
            if (!context.canceled) return;

            StateMachine.ChangeState(Owner.IdleState);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            //? allow the player to move freely on the stairs.
            if(Owner.IsOnStairs()){
                // Owner.Rigidbody.gravityScale = 0;
                Owner.Velocity = new Vector2(GameplayInput.MoveDirection.x * Owner.Settings.Speed, GameplayInput.MoveDirection.y * Owner.Settings.Speed);
                return;
            }
            //? if posible make some other form to go down the stairs, but for the prototipe this might work.
            /* other possible solutions might be to manually set other platform collider at the top of the stairs or
               search for a way to automatically do that.*/
            if(Owner.IsOverStairs()){
                if(GameplayInput.MoveDirection.y < 0){
                    float downJump = .5f;
                    Owner.MovePosition(Owner.Rigidbody.position + GameplayInput.MoveDirection * downJump);
                }
            }
            Owner.Rigidbody.gravityScale = Owner.Settings.OriginalGravityScale;
            Vector2 directionVector = Owner.VerifyDirectionVector();
            Owner.Velocity = Owner.Direction * Owner.Settings.Speed * directionVector;
        }

        public override string ToString() => nameof(MoveState);
    }
}
