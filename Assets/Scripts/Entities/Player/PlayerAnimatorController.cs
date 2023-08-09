using Entities.Player.States;
using UnityEngine;

namespace Entities.Player
{
    [RequireComponent(typeof(Animator))]
    public sealed class PlayerAnimatorController : MonoBehaviour
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Death = Animator.StringToHash("Death");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            IdleState.Started += OnIdleStateStarted;
            MoveState.Started += OnMoveStateStarted;
            JumpState.Started += OnJumpStateStarted;
        }

        private void OnDisable()
        {
            IdleState.Started -= OnIdleStateStarted;
            MoveState.Started -= OnMoveStateStarted;
            JumpState.Started -= OnJumpStateStarted;
        }

        private void OnIdleStateStarted(IdleState idleState)
        {
            _animator.Play(Idle);
        }

        private void OnMoveStateStarted(MoveState moveState)
        {
            _animator.Play(Move);
        }

        private void OnJumpStateStarted(JumpState state)
        {
            _animator.Play(Jump);
        }
    }
}
