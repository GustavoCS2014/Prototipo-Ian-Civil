using System;
using Core;

namespace Entities.Boss.States
{
    public class ShootState : State<BossController>
    {
        public ShootState(BossController owner, StateMachine<BossController> stateMachine) : base(owner, stateMachine) { }

        public static event Action<ShootState> Started;

        public static event Action<ShootState> Ended;

        public override void OnStart()
        {
            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            Ended?.Invoke(this);
        }

        public override string ToString() => nameof(ShootState);
    }
}
