using Entities.Boss.States;
using Entities.Player.States;
using UnityEngine;
using IdleState = Entities.Player.States.IdleState;
using JumpState = Entities.Player.States.JumpState;

namespace Entities.Player
{
    [RequireComponent(typeof(Animator))]
    public sealed class PlayerAnimatorController : MonoBehaviour
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Die = Animator.StringToHash("Die");

        [SerializeField, Range(0f, 1f)] private float transitionTime;

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
            DieState.Started += OnDieStateStarted;
        }

        private void OnDisable()
        {
            IdleState.Started -= OnIdleStateStarted;
            MoveState.Started -= OnMoveStateStarted;
            JumpState.Started -= OnJumpStateStarted;
            DieState.Started -= OnDieStateStarted;
        }

        private void OnIdleStateStarted(IdleState idleState)
        {
            _animator.CrossFade(Idle, transitionTime);
        }

        private void OnMoveStateStarted(MoveState moveState)
        {
            _animator.CrossFade(Move, transitionTime);
        }

        private void OnJumpStateStarted(JumpState state)
        {
            _animator.CrossFade(Jump, transitionTime);
        }

        private void OnDieStateStarted(DieState state)
        {
            _animator.CrossFade(Die, transitionTime);
        }
    }
}
