using System;
using Core;
using UnityEngine;
using Utilities;

namespace Entities.Boss.States
{
    public sealed class JumpState : State<BossController>
    {
        public JumpState(BossController owner, StateMachine<BossController> stateMachine) : base(owner, stateMachine) { }

        public static event Action<JumpState> Started;

        public static event Action<JumpState> Ended;

        public override void OnStart()
        {
            Owner.FacePlayer();

            var force = new Vector2
            {
                x = Owner.Settings.JumpDisplacement.x * Owner.Direction,
                y = PhysicsCalculator.JumpStrength(Owner.Rigidbody, Owner.Settings.JumpDisplacement.y)
            };
            Owner.Rigidbody.AddForce(force, ForceMode2D.Impulse);

            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            Ended?.Invoke(this);
        }

        public override void FixedUpdate()
        {
            if (Owner.Rigidbody.velocity.y <= 0f)
                StateMachine.ChangeState(Owner.FallState);
        }

        public override string ToString() => nameof(JumpState);
    }
}
