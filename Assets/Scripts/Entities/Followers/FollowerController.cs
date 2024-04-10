using Entities.Follower.States;
using Entities.Player;
using UnityEngine;

namespace Entities.Follower{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FollowerController : BaseEntityController<FollowerController> {
        public Transform Target;

        public FollowerSettings Settings => settings as FollowerSettings;

        public IdleState IdleState {get; protected set;}
        public FollowState FollowState {get; protected set;}

        protected override void Awake()
        {
            base.Awake();
            // THIS IS SUPER HARD CODED BUT I GOT LAZY
            // ignores collisions between NPCs (Layer 15)
            Physics2D.IgnoreLayerCollision(15,15);
            IdleState = new IdleState(this, StateMachine);
            FollowState = new FollowState(this, StateMachine);
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Settings.IdleArea);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, Settings.WalkingArea);
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, Settings.RunningArea);
        }

        protected virtual void Start() {
            StateMachine.Initialize(IdleState);
        }

        public virtual void SetTarget(Transform newTarget) => Target = newTarget; 
        public virtual void DeleteTarget() => Target = null;

        protected override void Update()
        {
            base.Update();

            UpdateGravity();
        }

        public virtual Vector2 TargetDistance(){
            if(Target == null) return Vector2.zero;
            return Target.position - transform.position;
        }

        public virtual void MoveToDirection(Vector3 direction){
            Velocity = Grounded ? direction :
                        new Vector2(direction.x, Velocity.y);
        }

        #region  STAIRS
        private void UpdateGravity() => Rigidbody.gravityScale = IsOnStairs() ? 
                                        0f :
                                        Settings.OriginalGravityScale;

        public bool IsOnStairs(){
            bool isOnStairs = false;
            float castRadius = .25f;
            Vector2 castPosition = transform.position + Vector3.up * (castRadius + .1f);
            Collider2D stairsCollider = Physics2D.OverlapCircle(castPosition, castRadius, Settings.StairsMask);

            if(stairsCollider) isOnStairs = true;
            return isOnStairs;
        }

        public bool IsOverStairs(){
            bool isOverStairs = false;
            float feetOffset = .2f;
            float rayDistance = .3f;
            Vector2 rayPos = transform.position + Vector3.up * feetOffset;

            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.down, rayDistance, Settings.StairsMask);
            
            if(hit && !IsOnStairs()) isOverStairs = true;

            return isOverStairs;
        }
        #endregion

    }

}