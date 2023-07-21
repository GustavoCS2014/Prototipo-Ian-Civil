using System;
using Core;

namespace Player.States
{
    public sealed class MoveState : PlayerState
    {
        public MoveState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

        public static event Action<MoveState> Started;

        public static event Action<MoveState> Ended;

        public override void OnStart()
        {
            Player.IdleState.OnEnd();
            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            Ended?.Invoke(this);
        }

        public override string ToString() => nameof(MoveState);
    }
}
