using System;
using Core;

namespace Entities.Boss.States
{
    public class DashState : State<BossController>
    {
        public DashState(BossController owner, StateMachine<BossController> stateMachine) : base(owner, stateMachine) { }

        public static event Action<DashState> Started;

        public static event Action<DashState> Ended;

        public override void OnStart()
        {
            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            Ended?.Invoke(this);
        }

        public override string ToString() => nameof(DashState);
    }
}
