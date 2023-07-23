using System;
using Core;
using UnityEngine;

namespace Entities.Boss.States
{
    public class IdleState : State<BossController>
    {
        private float _idleTime;

        public IdleState(BossController owner, StateMachine<BossController> stateMachine) : base(owner, stateMachine) { }

        public static event Action<IdleState> Started;

        public static event Action<IdleState> Ended;

        public override void OnStart()
        {
            Owner.FacePlayer();

            _idleTime = Time.time + Owner.Settings.IdleTime.Random;
            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            Ended?.Invoke(this);
        }

        public override void Update()
        {
            if (Time.time >= _idleTime)
                StateMachine.ChangeState(Owner.DashState);
        }

        public override string ToString() => nameof(IdleState);
    }
}
