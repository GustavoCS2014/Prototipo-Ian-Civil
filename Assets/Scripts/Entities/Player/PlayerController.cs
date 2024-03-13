using System;
using System.ComponentModel;
using Attributes;
using Cinematics;
using Entities.Player.States;
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
        public StairsState StairsState {get; private set;}

        public bool Animating { get; set; }
        public bool OnStairs;
        // public bool OnStairs {get; private set;}

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
            IdleState = new IdleState(this, StateMachine);
            MoveState = new MoveState(this, StateMachine);
            JumpState = new JumpState(this, StateMachine);
            FallState = new FallState(this, StateMachine);
            LandState = new LandState(this, StateMachine);
            StairsState = new StairsState(this, StateMachine);
            OnStairs = false;
        }

        private void Start()
        {
            StateMachine.Initialize(IdleState);
        }

        protected override void Update() {
            base.Update();
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            if (Settings)
                Gizmos.DrawRay(transform.position, Settings.JumpHeight * Vector3.up);
        }

        public void ChangeState(string state)
        {
            StateMachine.ChangeState(state switch
            {
                "Idle" => IdleState,
                "Move" => MoveState,
                "Jump" => JumpState,
                "Fall" => FallState,
                "Stairs" => StairsState,
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

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.TryGetComponent<BackgroundStairs>(out BackgroundStairs stairs)){
                OnStairs = true;
                Rigidbody.gravityScale = 0;
            }
        }
        private void OnTriggerStay2D(Collider2D other) {
            if(other.TryGetComponent<BackgroundStairs>(out BackgroundStairs stairs)){
                OnStairs = true;
            }
        }
        private void OnTriggerExit2D(Collider2D other) {
            if(other.TryGetComponent<BackgroundStairs>(out BackgroundStairs stairs)){
                OnStairs = false;
                Rigidbody.gravityScale = Settings.OriginalGravityScale;
            }
        }

        public bool IsOverStairs(){
            bool isOverStairs = false;
            float feetOffset = .2f;
            float rayDistance = .3f;
            Vector2 rayPos = transform.position + Vector3.up * feetOffset;
            RaycastHit2D hit;

            hit = Physics2D.Raycast(rayPos, Vector2.down, rayDistance, stairsMask);
            Debug.DrawRay(rayPos, Vector3.down * rayDistance, Color.green);
            if(hit && !OnStairs) isOverStairs = true;
            
            return isOverStairs;
        }

    }
}
