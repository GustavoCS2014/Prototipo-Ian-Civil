using System;
using Core;
using UnityEngine;
using Utilities;

namespace Entities.Boss.States
{
    public sealed class DashState : State<BossController>
    {
        private Vector2 _initialPosition;
        private Vector2 _targetPosition;
        private float _lerpTimer;
        private float _dashTime;

        public DashState(BossController owner, StateMachine<BossController> stateMachine) : base(owner, stateMachine) { }

        public static event Action<DashState> Started;

        public static event Action<DashState> Ended;

        public override void OnStart()
        {
            Owner.FacePlayer();

            _lerpTimer = 0f;
            _dashTime = Time.time + Owner.Settings.DashTime;
            _initialPosition = Owner.transform.position;
            _targetPosition = _initialPosition + Vector2.right * (Owner.Direction * Owner.Settings.DashDistance);

            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            Ended?.Invoke(this);
        }

        public override void Update()
        {
            if (Time.time >= _dashTime)
                StateMachine.ChangeState(Owner.IdleState);
        }

        public override void FixedUpdate()
        {
            float t = _lerpTimer / Owner.Settings.DashTime;

            Owner.MovePosition(Vector2.Lerp(
                _initialPosition, _targetPosition,
                Owner.Settings.DashCurve(t).Clamp01())
            );

            _lerpTimer += Time.fixedDeltaTime;
        }

        public override string ToString() => nameof(DashState);
    }
}
