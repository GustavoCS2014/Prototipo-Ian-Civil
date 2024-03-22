using System;
using System.ComponentModel;
using Core;
using UnityEngine;

namespace Entities.Follower{
    public class IdleState : State<FollowerController>
    {
        public IdleState(FollowerController owner, StateMachine<FollowerController> stateMachine) : base(owner, stateMachine) {}
        
        public static event Action<IdleState> Started;

        public static event Action<IdleState> Ended;

        public override void OnStart()
        {
            base.OnStart();
            Owner.Velocity = Vector2.zero;
            Started?.Invoke(this);
        }

        public override void OnEnd()
        {
            base.OnEnd();
            Ended?.Invoke(this);
        }

        public override void Update()
        {
            base.Update();

            Debug.DrawLine(Owner.transform.position, Owner.Target.transform.position, Color.blue);

            // Debug.Log($"Vel: {Owner.Velocity.x}");
            // if(!Owner.IsTargetFar()  && Mathf.Abs(Owner.Velocity.x) > 0){
            //     Owner.Velocity = MathF.Abs(Owner.Velocity.x) <= .05f? Vector2.zero :
            //         Vector2.Lerp(Owner.Velocity, Vector2.zero, Time.deltaTime);
            // }

            if(Owner.TargetDistance().magnitude > Owner.Settings.IdleArea){
                StateMachine.ChangeState(Owner.FollowState);
            }
        }

    }
}
