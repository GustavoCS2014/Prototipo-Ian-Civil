using System;
using Core;
using UnityEngine;

namespace Entities.Follower{
    public class FollowState : State<FollowerController>
    {
        public FollowState(FollowerController owner, StateMachine<FollowerController> stateMachine) : base(owner, stateMachine) {}
        
        public static event Action<FollowState> Started;

        public static event Action<FollowState> Ended;

        private bool _horizontalRunning;
        private bool _verticalRunning;
        private float _timeInactive;
        private float _maxInactiveTime = .5f;
        private Collider2D _ownerCollider;

        public override void OnStart()
        {
            base.OnStart();
            Started?.Invoke(this);
            _ownerCollider = Owner.GetComponent<Collider2D>();
        }

        public override void OnEnd()
        {
            base.OnEnd();
            Ended?.Invoke(this);
        }

        public override void Update()
        {
            base.Update();
            //? Resets the collider if enough time has passed 
            _timeInactive += Time.deltaTime;
            if(_timeInactive > _maxInactiveTime){
                if(_ownerCollider.enabled) return;
                _ownerCollider.enabled = true;
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(Mathf.Abs(Owner.TargetDistance().magnitude) < Owner.Settings.IdleArea * .8f){
                StateMachine.ChangeState(Owner.IdleState);
            }
            
            if(!Owner.IsOnStairs()) FollowHorizontal();
            GoUpstairs();
            
        }

        private void GoUpstairs()
        {
            //? if the character is over the stairs allows them to descend.
            bool canDescendStairs = false;
            if(Owner.IsOverStairs() && GetTargetDirection().y < 0f){
                DisableCollider();
                canDescendStairs = true;
            }

            //? if it aint in the stairs or above, stop.
            if(!Owner.IsOnStairs() && !canDescendStairs) return;

            //? go up faster if the target get's to far.
            if(Owner.TargetDistance().y > Owner.Settings.RunningArea && !_verticalRunning){
                _verticalRunning = true;
                return;
            }

            //? walk if on range and stop running.
            if(Owner.TargetDistance().y < Owner.Settings.WalkingArea){
                _verticalRunning = false;
                Owner.MovePosition(Owner.transform.position + GetTargetDirection() * Owner.Settings.WalkingSpeed * Time.deltaTime);
                return;
            }
            //? keep running boi
            if(_verticalRunning){
                Owner.MovePosition(Owner.transform.position + GetTargetDirection() * Owner.Settings.RunningSpeed* Time.deltaTime);
                return;
            }
            Owner.MovePosition(Owner.transform.position + GetTargetDirection() * Owner.Settings.WalkingSpeed* Time.deltaTime);

        }

        private void FollowHorizontal(){
            //? makes the character start running if the target get's too far.
            if(Owner.TargetDistance().x > Owner.Settings.RunningArea && !_horizontalRunning){
                _horizontalRunning = true;
                return;
            }

            Vector2 moveDirection = Owner.VerifyDirectionVector(2f);

            //? Stops the character if running and starts walking.
            if(Owner.TargetDistance().x < Owner.Settings.WalkingArea){
                _horizontalRunning = false;
                Owner.MoveToDirection(moveDirection * GetTargetDirection().x * Owner.Settings.WalkingSpeed);
                return;
            }

            //? if the character started running keep running.
            if(_horizontalRunning){
                Owner.MoveToDirection(moveDirection * GetTargetDirection().x * Owner.Settings.RunningSpeed);
                return;
            }

            //? Walk 
            Owner.MoveToDirection(moveDirection * GetTargetDirection().x * Owner.Settings.WalkingSpeed);
        }

        private Vector3 GetTargetDirection(){
            Vector2 outVector = Vector2.zero;
            Vector2 dir = Owner.Target.transform.position - Owner.transform.position;

            //? Pretty sure there's a better way to clamp this vector to fixed values without going to 0 at <.5f but
            //? this will do.

            if(dir.x > 0.1f) outVector += Vector2.right;
            if(dir.x < -0.1f) outVector += Vector2.left;
            if(dir.y > 0.1f) outVector += Vector2.up;
            if(dir.y < -0.1f) outVector += Vector2.down;

            return outVector;
        }

        //? PROBABLY REALLY BAD, but easy to program so, fk it
        private void DisableCollider(){
            _ownerCollider.enabled = false;
            _timeInactive = 0;
        }

    }
}
