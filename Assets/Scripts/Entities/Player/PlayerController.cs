using System;
using System.Collections.Generic;
using Cinematics;
using Entities.Follower;
using Entities.Player.States;
using Input;
using UnityEngine;

namespace Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerController : BaseEntityController<PlayerController>, IAnimable
    {
        public static PlayerController Instance { get; private set; }

        public PlayerSettings Settings => settings as PlayerSettings;

        public IdleState IdleState { get; private set; }
        public MoveState MoveState { get; private set; }
        public JumpState JumpState { get; private set; }
        public FallState FallState { get; private set; }
        public LandState LandState { get; private set; }
        public bool OnStairs {get; private set;}
        public bool Animating { get; set; }
        public List<FollowerController> Followers;

        protected override void Awake()
        {
            if(Instance){
                Debug.Log($"Destroyed {gameObject.name}");
                Destroy(gameObject);
            }else{
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            base.Awake();
            // THIS IS SUPER HARD CODED BUT I GOT LAZY
            // ignores collisions between Layer 6: player and Layer 15: NPC
            Physics2D.IgnoreLayerCollision(6,15);
            // Instance = this;
            IdleState = new IdleState(this, StateMachine);
            MoveState = new MoveState(this, StateMachine);
            JumpState = new JumpState(this, StateMachine);
            FallState = new FallState(this, StateMachine);
            LandState = new LandState(this, StateMachine);
            OnStairs = false;
        }

        private void Start()
        {
            StateMachine.Initialize(IdleState);
        }

        protected override void Update() {
            base.Update();
            // Debug.Log($"Instance {Instance.name}");
            UpdateGravity();
            if(GameplayInput.MoveDirection.y < 0 && IsOverStairs()){
                DisableStairsCollider();
            }
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            if (Settings && showDebug)
                Gizmos.DrawRay(transform.position, Settings.JumpHeight * Vector3.up);

            if(showDebug){
                float castRadius = .25f;
                Vector2 castPosition = transform.position + Vector3.up * (castRadius + .1f);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(castPosition, castRadius);
            }
        }

        public void ChangeState(string state)
        {
            StateMachine.ChangeState(state switch
            {
                "Idle" => IdleState,
                "Move" => MoveState,
                "Jump" => JumpState,
                "Fall" => FallState,
                _ => throw new Exception($"State {state} not found")
            });
        }

        public void FaceDirection(float direction)
        {
            Direction = direction;
        }

        public void Idle(float direction)
        {
            Direction = direction;
            StateMachine.ChangeState(IdleState);
            Direction = 0f;
        }

        public void MoveTo(Transform target)
        {
            transform.position = target.position;
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

        public void DisableStairsCollider(){
            float feetOffset = .2f;
            float rayDistance = .3f;
            Vector2 rayPos = transform.position + Vector3.up * feetOffset;

            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.down, rayDistance, Settings.StairsMask);
            

            if(hit.transform.TryGetComponent<StairTopHandler>(out StairTopHandler stairTop)){
                stairTop.DisableCollider();
            }
        }
        #endregion


    }
}
