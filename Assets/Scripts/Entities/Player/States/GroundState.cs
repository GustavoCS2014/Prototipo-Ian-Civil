using Core;
using Input;
using UnityEngine.InputSystem;

namespace Entities.Player.States
{
    public abstract class GroundState : PlayerState
    {
        protected GroundState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

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

            if (!Player.Grounded) return;

            StateMachine.ChangeState(Player.JumpState);
        }

        public override void FixedUpdate()
        {
            if (!Player.Grounded)
                StateMachine.ChangeState(Player.FallState);
        }
    }
}
