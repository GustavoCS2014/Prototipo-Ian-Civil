using System;
using Core;

namespace Entities.Boss.States
{
    public class IdleState : State<BossController>
    {
        public IdleState(BossController owner, StateMachine<BossController> stateMachine) : base(owner, stateMachine) { }

        public static event Action<IdleState> Started;

        public static event Action<IdleState> Ended;

        public override void OnStart()
        {
            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            Ended?.Invoke(this);
        }

        public override string ToString() => nameof(IdleState);
    }
}
