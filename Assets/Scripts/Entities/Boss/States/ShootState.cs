using System;
using System.Collections;
using Core;
using UnityEngine;

namespace Entities.Boss.States
{
    public sealed class ShootState : State<BossController>
    {
        private float _shootTime;

        public ShootState(BossController owner, StateMachine<BossController> stateMachine) : base(owner, stateMachine) { }

        public static event Action<ShootState> Started;

        public static event Action<ShootState> Ended;

        public override void OnStart()
        {
            Owner.FacePlayer();

            if (Owner.Shooter)
                Owner.StartCoroutine(Shoot(Owner.Settings.TimesToShoot));

            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            Ended?.Invoke(this);
        }

        private IEnumerator Shoot(int timesToShoot)
        {
            for (var i = 0; i < timesToShoot; i++)
            {
                Owner.Shooter.Shoot(Owner.FacingDirection);
                yield return new WaitForSeconds(Owner.Settings.TimeBetweenShots);
            }

            yield return new WaitForSeconds(Owner.Settings.ShootCooldownTime);
            StateMachine.ChangeState(Owner.IdleState);
        }

        public override string ToString() => nameof(ShootState);
    }
}
