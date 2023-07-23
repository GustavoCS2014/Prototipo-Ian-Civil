using System;
using Core;
using UnityEngine;
using Utilities;

namespace Entities.Boss.States
{
    public sealed class IdleState : State<BossController>
    {
        private float _idleTime;

        public IdleState(BossController owner, StateMachine<BossController> stateMachine) : base(owner, stateMachine) { }

        public static event Action<IdleState> Started;

        public static event Action<IdleState> Ended;

        public override void OnStart()
        {
            Owner.Rigidbody.velocity = Vector2.zero;
            Owner.FacePlayer();

            _idleTime = Time.time + Owner.Settings.IdleTime;
            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            Ended?.Invoke(this);
        }

        public override void Update()
        {
            if (Time.time >= _idleTime)
                StateMachine.ChangeState(CoolerRandom.Int(3) switch
                {
                    0 => Owner.DashState,
                    1 => Owner.ShootState,
                    _ => Owner.JumpState
                });
        }

        public override string ToString() => nameof(IdleState);
    }
}
