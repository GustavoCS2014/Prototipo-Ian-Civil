using System;
using Core;

namespace Entities.Boss.States
{
    public sealed class FallState : State<BossController>
    {
        public FallState(BossController owner, StateMachine<BossController> stateMachine) : base(owner, stateMachine) { }

        public static event Action<FallState> Started;

        public static event Action<FallState> Ended;

        public override void OnStart()
        {
            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            Ended?.Invoke(this);
        }

        public override void FixedUpdate()
        {
            if (Owner.Grounded)
                StateMachine.ChangeState(Owner.IdleState);
        }

        public override string ToString() => nameof(FallState);
    }
}
