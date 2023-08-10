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
        private static readonly int Land = Animator.StringToHash("Land");

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
            LandState.Started += OnLandStateStarted;
        }

        private void OnDisable()
        {
            IdleState.Started -= OnIdleStateStarted;
            MoveState.Started -= OnMoveStateStarted;
            JumpState.Started -= OnJumpStateStarted;
            LandState.Started -= OnLandStateStarted;
        }

        private void OnIdleStateStarted(IdleState state)
        {
            _animator.CrossFade(Idle, transitionTime);
        }

        private void OnMoveStateStarted(MoveState state)
        {
            _animator.CrossFade(Move, transitionTime);
        }

        private void OnJumpStateStarted(JumpState state)
        {
            _animator.CrossFade(Jump, transitionTime);
        }

        private void OnLandStateStarted(LandState state)
        {
            _animator.CrossFade(Land, transitionTime);
        }
    }
}
