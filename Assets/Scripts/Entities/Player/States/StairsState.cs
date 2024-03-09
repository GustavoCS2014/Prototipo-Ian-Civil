using System;
using Core;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player.States{
    public sealed class StairsState : GroundState
    {
        public StairsState(PlayerController player, StateMachine<PlayerController> stateMachine) : base(player, stateMachine) {}
    
        public static event Action<StairsState> Started;
        public static event Action<StairsState> Ended;

        private float originalGravity;
        private Vector2 inputDirection;

        public override void OnStart()
        {
            base.OnStart();
            GameplayInput.OnMove += OnMoveInput;
            originalGravity = Owner.Rigidbody.gravityScale;
            Owner.Rigidbody.gravityScale = 0;
            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            base.OnEnd();
            GameplayInput.OnMove -= OnMoveInput;
            Owner.Rigidbody.gravityScale = originalGravity;
            Ended?.Invoke(this);
        }

        private void OnMoveInput(InputAction.CallbackContext context){
            inputDirection = context.ReadValue<Vector2>();
            if(!context.canceled) return;
            StateMachine.ChangeState(Owner.IdleState);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Vector2 directionVector = Owner.VerifyDirectionVector();
            Owner.Velocity = Owner.Direction * Owner.Settings.Speed * directionVector;
            Owner.MovePosition(new Vector2(Owner.transform.position.x, inputDirection.y * Owner.Settings.Speed*Time.deltaTime));
        }

    }
}
