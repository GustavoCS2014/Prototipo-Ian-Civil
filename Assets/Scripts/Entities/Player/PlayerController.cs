using System;
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

        public bool Animating { get; set; }

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
            IdleState = new IdleState(this, StateMachine);
            MoveState = new MoveState(this, StateMachine);
            JumpState = new JumpState(this, StateMachine);
            FallState = new FallState(this, StateMachine);
        }

        private void Start()
        {
            Initialize(IdleState);
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
                _ => throw new Exception($"State {state} not found")
            });
        }

        public void FaceDirection(float direction)
        {
            Direction = direction;
        }
    }
}
