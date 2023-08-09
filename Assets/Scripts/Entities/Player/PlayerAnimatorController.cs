using Core;
using Entities.Player.States;
using Media;
using UnityEngine;

namespace Entities.Player
{
    [RequireComponent(typeof(Animator))]
    public sealed class PlayerAnimatorController : MonoBehaviour
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Death = Animator.StringToHash("Death");

        [SerializeField] private EntityAnimationsMedia animationsMedia;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            IdleState.Started += OnIdleStateStarted;
            JumpState.Started += OnJumpStateStarted;
        }

        private void OnDisable()
        {
            IdleState.Started -= OnIdleStateStarted;
            JumpState.Started -= OnJumpStateStarted;
        }

        private void OnIdleStateStarted(IdleState idleState)
        {
            _animator.Play(Idle);
        }

        private void OnJumpStateStarted(JumpState state)
        {
            _animator.Play(Jump);
        }
    }
}
