using Core;
using Input;
using UnityEngine.InputSystem;

namespace Entities.Player.States
{
    public abstract class GroundState : State<PlayerController>
    {
        protected GroundState(PlayerController player, StateMachine<PlayerController> stateMachine) : base(player, stateMachine) { }

        public override void OnStart()
        {
            GameplayInput.OnJump += OnJumpInput;
        }

        public override void OnEnd()
        {
            GameplayInput.OnJump -= OnJumpInput;
        }

        private void OnJumpInput(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            if (!Owner.Grounded) return;

            StateMachine.ChangeState(Owner.JumpState);
        }

        public override void FixedUpdate()
        {
            if (!Owner.Grounded)
                StateMachine.ChangeState(Owner.FallState);
        }
    }
}
