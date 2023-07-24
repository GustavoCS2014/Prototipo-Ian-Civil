using System;
using Core;
using UnityEngine;

namespace Entities.Boss.States
{
    public class DieState : State<BossController>
    {
        private float _dieTime;

        public DieState(BossController owner, StateMachine<BossController> stateMachine) : base(owner, stateMachine) { }

        public static event Action<DieState> Started;

        public static event Action<DieState> Ended;

        public override void OnStart()
        {
            _dieTime = Time.time + Owner.Settings.DieTime;
            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            Ended?.Invoke(this);
        }

        public override void Update()
        {
            if (Time.time >= _dieTime)
                StateMachine.Kill();
        }

        public override string ToString() => nameof(DieState);
    }
}
